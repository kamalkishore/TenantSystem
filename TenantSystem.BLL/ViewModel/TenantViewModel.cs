using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenantSystem.BLL.ViewModel
{
    public class TenantViewModel
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string NickName { get; set; }
        public int PhoneNumber { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public ElectricMeterViewModel Meter { get; set; }
    }
}
