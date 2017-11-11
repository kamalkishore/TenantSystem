using System.Collections.Generic;
using System.Linq;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.BLL
{
    public class TenantLogic
    {
        private BillLogic _billLogic;
        private ITenantRepository _tenantRepo;

        public TenantLogic(BillLogic billLogic, ITenantRepository tenantRepo)
        {
            _billLogic = billLogic;
            _tenantRepo = tenantRepo;
        }

        public IEnumerable<dynamic> GetTenantHistory(Tenant tenant)
        {
            var tenantBills = _billLogic.GetBillsFor(tenant);

            var result = from bill in tenantBills
                         select new
                         {
                             PaymentAmount = bill.LastPaidAmount,
                             PaymentDate = bill.LastPaidAmountDate,
                             CurrentMonthMeterReading = bill.CurrentReading,
                             CurrentMonthMeterReadingDate = bill.CurrentReadingDate,
                             PreviousMonthMeterReadingDate = bill.PreviousReadingDate,
                             PreviousMonthMeterReading = bill.PreviousReading,
                             AmountPayable = bill.CurrentPayableAmount,
                             PerUnitPrice = bill.PricePerUnit,
                             TotalAmountPayable = bill.TotalAmount
                         };

            return result;
        }
    }
}
