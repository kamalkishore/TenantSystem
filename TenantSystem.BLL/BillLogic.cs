using System;
using System.Collections.Generic;
using System.Linq;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.BLL
{
    public class BillLogic
    {
        private IBillRepository _billRepo;

        public BillLogic(IBillRepository billRepostory)
        {
            _billRepo = billRepostory;
        }

        public IEnumerable<BillViewModel> GetAllBillsFor(Tenant tenant)
        {
            var bills = _billRepo.GetAll(tenant);
            return GetBillList(bills);
        }

        public IEnumerable<BillViewModel> GetAllBillsFor(string month, string year)
        {
            var bills =  _billRepo
                        .GetAll()
                        .Where(x=> (x.CurrentReadingDate.Month.Equals(int.Parse(month))
                                && (x.CurrentReadingDate.Year.Equals(int.Parse(year)))));

            return GetBillList(bills);
        }

        public IEnumerable<BillViewModel> GetAllBillsFor(Tenant tenant, string month, string year)
        {
            var tenantBills = _billRepo.GetAll(tenant);

            var bills = tenantBills
                        .Where(x => (x.CurrentReadingDate.Month.Equals(int.Parse(month))
                                && (x.CurrentReadingDate.Year.Equals(int.Parse(year)))));

            return GetBillList(bills);
        }

        private IEnumerable<BillViewModel> GetBillList(IEnumerable<Bill> bills)
        {
            return bills.Select(x => new BillViewModel
            {
                Id = x.Id,
                TenantName = x.Tenant.FirstName + " " + x.Tenant.LastName,
                CurrentMonthReadingDate = x.CurrentReadingDate,
                CurrentMonthReading = x.CurrentReading,
                PreviousMonthReadingDate = x.PreviousReadingDate,
                PreviousMonthReading = x.PreviousReading,
                UnitConsumed = x.UnitConsumed,
                PerUnitPrice = x.PricePerUnit,
                CurrentMonthPayableAmount = x.CurrentPayableAmount,
                PreviousMonthPendingAmount = x.PreviousPendingAmount,
                LastPaidAmountDate = x.LastPaidAmountDate,
                LastPaidAmount = x.LastPaidAmount,
                TotalPayableAmount = x.TotalAmount,
                Year = x.CurrentReadingDate.Year
            })
            .OrderByDescending(y => y.CurrentMonthReadingDate)
            .ToList();
        }

        public IEnumerable<BillViewModel> GetAllBills()
        {
            return _billRepo.GetAll().Select(x=> new BillViewModel
            {
                Id = x.Id,
                CurrentMonthPayableAmount = x.CurrentPayableAmount,
                CurrentMonthReading = x.CurrentReading,
                CurrentMonthReadingDate = x.CurrentReadingDate,
                PreviousMonthPendingAmount = x.PreviousPendingAmount,
                LastPaidAmount = x.LastPaidAmount,
                LastPaidAmountDate = x.LastPaidAmountDate,
                PerUnitPrice = x.PricePerUnit,
                PreviousMonthReading = x.PreviousReading,
                PreviousMonthReadingDate = x.PreviousReadingDate,
                TenantName = x.Tenant.FirstName + " " + x.Tenant.LastName,
                TotalPayableAmount = x.TotalAmount,
                UnitConsumed = x.UnitConsumed,
                Year = x.CurrentReadingDate.Year

            });
        }

        public IEnumerable<BillViewModel> GetBillsFor(Tenant tenant)
        {
            return _billRepo.GetAll(tenant).Select(x => new BillViewModel
            {
                Id = x.Id,
                CurrentMonthPayableAmount = x.CurrentPayableAmount,
                CurrentMonthReading = x.CurrentReading,
                CurrentMonthReadingDate = x.CurrentReadingDate,
                PreviousMonthPendingAmount = x.PreviousPendingAmount,
                LastPaidAmount = x.LastPaidAmount,
                LastPaidAmountDate = x.LastPaidAmountDate,
                PerUnitPrice = x.PricePerUnit,
                PreviousMonthReading = x.PreviousReading,
                PreviousMonthReadingDate = x.PreviousReadingDate,
                TenantName = x.Tenant.FirstName + " " + x.Tenant.LastName,
                TotalPayableAmount = x.TotalAmount,
                UnitConsumed = x.UnitConsumed,
                Year = x.CurrentReadingDate.Year

            });
        }
    }
}
