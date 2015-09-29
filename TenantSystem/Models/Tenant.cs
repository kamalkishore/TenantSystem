using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("MiddleName")]
        public string MiddleName { get; set; }        
        
        [Required]
        [DataType(DataType.Text)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }        
        
        [DataType(DataType.Text)]
        public string NickName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        
        public int MeterId { get; set; }
                
        public ElectricMeter Meter { get; set; }
        public virtual ICollection<TenantMeterReading> MeterReading { get; set; }
        public virtual ICollection<TenantPayment> Payments { get; set; }
        public virtual ICollection<TenantBill> Bills { get; set; }
        public virtual ICollection<TenantPreviousReadingDetails> PreviousReadingDetails { get; set; }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
            set { value = this.FirstName + " " + this.LastName; }
        }

        //todo why this method does not work in linq
        public bool DoesPendingBillExist()
        {
            return MeterReading.OrderBy(y => y.Id).Where(z => z.DoesBillGenerated != true).Count() > 0;
        }

        public double GetAmountReceivable()
        {
            return Bills.
                Select(x=>x.CurrentMonthPayableAmount)
                .DefaultIfEmpty()
                .Sum() - Payments.Select(x=>x.Amount).DefaultIfEmpty().Sum();

        }

        public TenantMeterReading GetMeterReadingDetails()
        {
            return MeterReading.OrderBy(y => y.Id).Where(z => z.DoesBillGenerated != true).FirstOrDefault();
        }

        public TenantPayment GetLastPaymentDetailsBefore(DateTime date)
        {
            return Payments.Where(z => z.PaymentType != PaymentType.BadDebt && z.DateOfPayment < date)
                                    .OrderByDescending(y => y.DateOfPayment)
                                    .FirstOrDefault();
        }

        public double GetPreviousPendingAmount(DateTime date)
        {
            return MeterReading
                        .Where(x=>x.DateOfMeterReading < date)
                        .Select(y => y.AmountPayable).DefaultIfEmpty().Sum() -
                        Payments.Where(x=>x.DateOfPayment < date).Select(y => y.Amount).DefaultIfEmpty().Sum();
        }

        public dynamic GetPendingBillToApprove()
        {
            var meterReadingDetails = GetMeterReadingDetails();
            var previousPendingAmount = GetPreviousPendingAmount(meterReadingDetails.DateOfMeterReading);
            var lastPaymentDetails = GetLastPaymentDetailsBefore(meterReadingDetails.DateOfMeterReading);

            return  new 
                            {
                                Id = meterReadingDetails.Id,
                                TenantId = Id,
                                PreviousMonthReading = meterReadingDetails.PreviousMeterReading,
                                PreviousMonthReadingDate = meterReadingDetails.DateOfPreviousMonthMeterReading,
                                CurrentMonthReading = meterReadingDetails.CurrentMeterReading,
                                CurrentMonthPayableAmount = meterReadingDetails.AmountPayable,
                                CurrentMonthReadingDate = meterReadingDetails.DateOfMeterReading,
                                UnitConsumed = meterReadingDetails.CurrentMeterReading - meterReadingDetails.PreviousMeterReading,
                                LastPaidAmount = (lastPaymentDetails == null) ? 0 : lastPaymentDetails.Amount,
                                LastPaidAmountDate = (lastPaymentDetails == null) ? DateTime.MinValue : lastPaymentDetails.DateOfPayment,//todo need to display the blank if date is not present
                                PreviousMonthPendingAmount = previousPendingAmount,
                                TotalPayableAmount = previousPendingAmount + meterReadingDetails.AmountPayable,
                                TenantName = this.FullName,
                                PerUnitPrice = meterReadingDetails.PerUnitPrice,
                                MonthName = CultureInfo.CurrentCulture.DateTimeFormat
                                                                                     .GetMonthName(meterReadingDetails.DateOfPreviousMonthMeterReading.Month)
                            };
        }



        public TenantBill GetBillDetailsOf(string month, string year)
        {
            return Bills
                    .Where(x => (x.PreviousMonthReadingDate.Month == int.Parse(month))
                                && (x.CurrentMonthReadingDate.Year.ToString() == year))
                    .FirstOrDefault();
        }

        public void UpdatePendingBill(TenantMeterReading tenantMeterReading)
        {
            var updateMeterReading = MeterReading.Where(x => x.Id == tenantMeterReading.Id).FirstOrDefault();

            updateMeterReading.PerUnitPrice = tenantMeterReading.PerUnitPrice;
            updateMeterReading.CurrentMeterReading = tenantMeterReading.CurrentMeterReading;
            updateMeterReading.AmountPayable = tenantMeterReading.AmountPayable;
            updateMeterReading.DateOfMeterReading = tenantMeterReading.DateOfMeterReading;

        }

        public void AddBill(dynamic pendingBill)
        {
            var tenantBill = new TenantBill
                                {
                                    TenantId = pendingBill.TenantId,
                                    PreviousMonthReading = pendingBill.PreviousMonthReading,
                                    PreviousMonthReadingDate = pendingBill.PreviousMonthReadingDate,
                                    CurrentMonthReading = pendingBill.CurrentMonthReading,
                                    CurrentMonthPayableAmount = pendingBill.CurrentMonthPayableAmount,
                                    CurrentMonthReadingDate = pendingBill.CurrentMonthReadingDate,
                                    UnitConsumed = pendingBill.UnitConsumed,
                                    LastPaidAmount = pendingBill.LastPaidAmount,
                                    LastPaidAmountDate = pendingBill.LastPaidAmountDate,
                                    PreviousMonthPendingAmount = pendingBill.PreviousMonthPendingAmount,
                                    TotalPayableAmount = pendingBill.TotalPayableAmount,
                                    TenantName = pendingBill.TenantName,
                                    PerUnitPrice = pendingBill.PerUnitPrice,
                                    MonthName = pendingBill.MonthName
                                };

            Bills.Add(tenantBill);
        }
    }
}