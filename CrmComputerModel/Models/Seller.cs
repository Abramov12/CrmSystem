using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class Seller
    {
        public int SellerId { get; set; }
        public string ?SellerName { get; set; }
        public Seller(string name)
        {
            SellerName = name;
        }
        public Seller()
        {
            
        }
        public virtual ICollection<Check> ?Checks { get; set; }
        public override string ToString()
        {
            return SellerName;
        }
    }
}
