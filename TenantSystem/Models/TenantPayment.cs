using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TenantSystem.Models
{
    public class TenantPayment
    {
        public long Id { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date Of Payment")]
        public DateTime DateOfPayment { get; set; }        
        public string Comments { get; set; }
        [Required]
        [DisplayName("Payment Type")]
        public PaymentType PaymentType { get; set; }
    }

    public enum PaymentType
    {
        Cash = 0,
        Cheque = 1,
        BadDebt = 2
    }
}