using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        public double GetAmountReceivable()
        {
            return Bills.
                Select(x=>x.CurrentMonthPayableAmount)
                .DefaultIfEmpty()
                .Sum() - Payments.Select(x=>x.Amount).DefaultIfEmpty().Sum();

        }
    }
}