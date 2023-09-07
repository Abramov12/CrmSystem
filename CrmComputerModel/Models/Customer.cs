using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<Check> ?Checks { get; set; }
        public Customer(string name)
        {
            CustomerName = name;
        }
        public Customer()
        {

        }
        public override string ToString()
        {
            return CustomerName;
        }
    }
}
