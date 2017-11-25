using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenantSystem.BLL.ViewModel;
using TenantSystem.Model.Interface;
using TenantSystem.Model.Model;

namespace TenantSystem.BLL
{
    public class ElectricMeterService
    {
        IElectricMeterRepository _meterRepo;
        public ElectricMeterService(IElectricMeterRepository meterRepo)
        {
            _meterRepo = meterRepo;
        }

        public int AddMeter(ElectricMeterViewModel meterView)
        {
            var meter = new ElectricMeter
            {
                InitialReading = meterView.InitialReading,
                CurrentMeterReading = meterView.CurrentMeterReading,
                DateOfCurrentMeterReading = meterView.DateOfCurrentMeterReading,
                DateOfMeterInstalled = meterView.DateOfMeterInstalled,
                IsOccupied = false,
                Name = meterView.Name,
                MeterType = meterView.MeterType
            };

            return _meterRepo.Add(meter);
        }

        public void UpdateMeter(ElectricMeterViewModel meterView)
        {
            var meter = new ElectricMeter
            {
                InitialReading = meterView.InitialReading,
                CurrentMeterReading = meterView.CurrentMeterReading,
                DateOfCurrentMeterReading = meterView.DateOfCurrentMeterReading,
                DateOfMeterInstalled = meterView.DateOfMeterInstalled,
                IsOccupied = false,
                Name = meterView.Name,
                MeterType = meterView.MeterType
            };

            _meterRepo.Update(meter);
        }

        public IEnumerable<ElectricMeterViewModel> GetAllMeters()
        {
            return _meterRepo.GetAll().Select(x => new ElectricMeterViewModel
            {
                Id = x.Id,
                CurrentMeterReading = x.CurrentMeterReading,
                DateOfCurrentMeterReading = x.DateOfCurrentMeterReading,
                DateOfMeterInstalled = x.DateOfMeterInstalled,
                InitialReading = x.InitialReading,
                IsOccupied = x.IsOccupied,
                MeterType = x.MeterType,
                Name = x.Name
            });
        }

        public ElectricMeterViewModel GetMeter(int id)
        {
            var meter = _meterRepo.Get(id);

            return new ElectricMeterViewModel
            {
                Id = meter.Id,
                CurrentMeterReading = meter.CurrentMeterReading,
                DateOfCurrentMeterReading = meter.DateOfCurrentMeterReading,
                DateOfMeterInstalled = meter.DateOfMeterInstalled,
                InitialReading = meter.InitialReading,
                IsOccupied = meter.IsOccupied,
                MeterType = meter.MeterType,
                Name = meter.Name
            };
        }
    }
}
