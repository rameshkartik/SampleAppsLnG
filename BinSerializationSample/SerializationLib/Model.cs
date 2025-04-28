using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationLib
{
    public class EmployeeModel
    {
        public string EmployeeName { get; set; } = string.Empty;
        public int EmployeeID2 { get; set;}
        public Address AddressObject { get; set; }  

    }

    public class BaseEmployeeModel : EmployeeModel { }

    public class Address
    {
        public string City { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;
    }
}
