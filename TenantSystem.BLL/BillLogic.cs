using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<dynamic> GetAllBillsFor(Tenant tenant)
        {
            var bills = _billRepo.GetAll(tenant);
            return GetBillList(bills);
        }

        public IEnumerable<dynamic> GetAllBillsFor(string month, string year)
        {
            var bills =  _billRepo
                        .GetAll()
                        .Where(x=> (x.CurrentReadingDate.Month.Equals(int.Parse(month))
                                && (x.CurrentReadingDate.Year.Equals(int.Parse(year)))));

            return GetBillList(bills);
        }

        public IEnumerable<dynamic> GetAllBillsFor(Tenant tenant, string month, string year)
        {
            var tenantBills = _billRepo.GetAll(tenant);

            var bills = tenantBills
                        .Where(x => (x.CurrentReadingDate.Month.Equals(int.Parse(month))
                                && (x.CurrentReadingDate.Year.Equals(int.Parse(year)))));

            return GetBillList(bills);
        }

        private IEnumerable<dynamic> GetBillList(IEnumerable<Bill> bills)
        {
            return bills.Select(x => new
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

        public IEnumerable<Bill> GetAllBills()
        {
            return _billRepo.GetAll();
        }

        public IEnumerable<Bill> GetBillsFor(Tenant tenant)
        {
            return _billRepo.GetAll(tenant);
        }
    }
}
