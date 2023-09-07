using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class Check
    {
        public int CheckId { get; set; }
        public int SellerID { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Sell> Sells { get; set; }
        public override string ToString()
        {
            return $"Чек с номером {CheckId}, от {Date.ToString("hh:mm:ss dd.MM.yyyy")}";
        }
    }
}
