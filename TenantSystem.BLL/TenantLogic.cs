using System;
using System.Collections.Generic;
using System.Linq;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.BLL
{
    public class TenantLogic
    {
        private BillLogic _billLogic;
        private ITenantRepository _tenantRepo;
        private IMeterReadingRepository _meterReadingRepo;
        IElectricMeterRepository _electricMeterRepo;
        IPreviousReadingDetailRepository _previousReadingRepo;

        public TenantLogic(BillLogic billLogic,
                           ITenantRepository tenantRepo,
                           IMeterReadingRepository meterReadingRepo,
                           IElectricMeterRepository electricMeterRepo,
                           IPreviousReadingDetailRepository previousReadingRepo)
        {
            _billLogic = billLogic;
            _tenantRepo = tenantRepo;
            _meterReadingRepo = meterReadingRepo;
            _electricMeterRepo = electricMeterRepo;
            _previousReadingRepo = previousReadingRepo;
        }

        public IEnumerable<dynamic> GetTenantHistory(TenantViewModel tenant)
        {
            var tenantData = _tenantRepo.Get(tenant.Id);
            //return _billLogic.GetBillsFor(tenantData);

            var result = from bill in _billLogic.GetBillsFor(tenantData)
                         select new 
                         {
                             PaymentAmount = bill.LastPaidAmount,
                             PaymentDate = bill.LastPaidAmountDate,
                             CurrentMonthMeterReading = bill.CurrentMonthReading,
                             CurrentMonthMeterReadingDate = bill.CurrentMonthReadingDate,
                             PreviousMonthMeterReadingDate = bill.PreviousMonthReadingDate,
                             PreviousMonthMeterReading = bill.PreviousMonthReading,
                             AmountPayable = bill.CurrentMonthPayableAmount,
                             PerUnitPrice = bill.PerUnitPrice,
                             TotalAmountPayable = bill.TotalPayableAmount
                         };

            return result;
        }

        public IEnumerable<TenantViewModel> GetTenants()
        {
            return _tenantRepo.GetAll()
                .Select(x => new TenantViewModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName})
                .ToList();
        }

        public TenantViewModel GetTenant(int id)
        {
            var tenant = _tenantRepo.Get(id);
            return new TenantViewModel
                        {
                            Id = tenant.Id,
                            FirstName = tenant.FirstName,
                            LastName = tenant.LastName,
                            PhoneNumber = tenant.PhoneNumber,
                            Meter = new ElectricMeterViewModel
                            {
                                Id = tenant.ElectricMeter.Id,
                                Name = tenant.ElectricMeter.Name,
                                CurrentMeterReading = tenant.ElectricMeter.CurrentMeterReading,
                                DateOfCurrentMeterReading = tenant.ElectricMeter.DateOfCurrentMeterReading,
                                DateOfMeterInstalled = tenant.ElectricMeter.DateOfMeterInstalled,
                                InitialReading = tenant.ElectricMeter.InitialReading,
                                IsOccupied = tenant.ElectricMeter.IsOccupied,
                                MeterType = tenant.ElectricMeter.MeterType
                            }
                        };
        }

        public void AddMeterReading(MeterReadingViewModel reading)
        {
            var tenant = _tenantRepo.Get(reading.Tenant.Id);

            var meterReading = new TenantMeterReading
            {
                MeterId = reading.MeterId,
                CurrentMeterReading = reading.CurrentMeterReading,
                AmountPayable = reading.AmountPayable,
                DateOfMeterReading = reading.DateOfMeterReading,
                DateOfPreviousMonthMeterReading = reading.DateOfPreviousMonthMeterReading,
                PreviousMeterReading = reading.PreviousMeterReading,
                PerUnitPrice = reading.PerUnitPrice,
                Tenant = tenant,
                DoesBillGenerated = false
            };

            _meterReadingRepo.Add(meterReading);

            var electricMeter = _electricMeterRepo.Get(meterReading.MeterId);

            //update meter
            electricMeter.DateOfCurrentMeterReading = meterReading.DateOfMeterReading;
            electricMeter.CurrentMeterReading = meterReading.CurrentMeterReading;

            var previousReading = _previousReadingRepo.Get(tenant);

            //todo add the default values in order to remove null logic
            if (previousReading != null)
            {
                previousReading.AmountPayable = meterReading.AmountPayable;
                previousReading.DateOfPreviousMonthMeterReading = meterReading.DateOfMeterReading;
                previousReading.MeterId = meterReading.MeterId;
                previousReading.PreviousMeterReading = meterReading.CurrentMeterReading;
                previousReading.PerUnitPrice = meterReading.PerUnitPrice;
                _previousReadingRepo.Update(previousReading);
            }
            else
            {
                previousReading = new TenantPreviousReadingDetails
                {
                    Tenant = tenant,
                    AmountPayable = meterReading.AmountPayable,
                    MeterId = meterReading.MeterId,
                    DateOfPreviousMonthMeterReading = meterReading.DateOfMeterReading,
                    PreviousMeterReading = meterReading.CurrentMeterReading,
                    PerUnitPrice = meterReading.PerUnitPrice
                };

                _previousReadingRepo.Add(previousReading);
            }
        }

        public Result AddTenant(TenantViewModel tenantViewModel)
        {
            if (tenantViewModel == null)
                return Result.Fail("Null object cannot be processed");
            if (tenantViewModel.Meter == null)
                return Result.Fail("Meter is not assigned");
            if (_electricMeterRepo.Get(tenantViewModel.Meter.Id) == null)
                return Result.Fail("Invalid meter is assigned");

            var newTenant = new Tenant();
            newTenant.FirstName = tenantViewModel.FirstName;
            newTenant.LastName = tenantViewModel.LastName;
            newTenant.MiddleName = tenantViewModel.MiddleName;
            newTenant.PhoneNumber = tenantViewModel.PhoneNumber;
            newTenant.ElectricMeter = _electricMeterRepo.Get(tenantViewModel.Meter.Id);

            _tenantRepo.Add(newTenant);

            return Result.Ok();
        }

        public Result Editenant(TenantViewModel tenantViewModel)
        {
            if (tenantViewModel == null)
                return Result.Fail("Null object cannot be processed");

            if (_tenantRepo.Get(tenantViewModel.Id) == null)
                return Result.Fail("Tenant does not exist");

            //if (tenantViewModel.Meter == null)
            //    return Result.Fail("Meter is not assigned");
            //if (_electricMeterRepo.Get(tenantViewModel.Meter.Id) == null)
            //    return Result.Fail("Invalid meter is assigned");

            var existingTenant = _tenantRepo.Get(tenantViewModel.Id);
            existingTenant.FirstName = tenantViewModel.FirstName;
            existingTenant.LastName = tenantViewModel.LastName;
            existingTenant.MiddleName = tenantViewModel.MiddleName;
            existingTenant.PhoneNumber = tenantViewModel.PhoneNumber;

            _tenantRepo.Update(existingTenant);
            return Result.Ok();
        }

        public Result GetPayments(int id)
        {

        }
    }
}
