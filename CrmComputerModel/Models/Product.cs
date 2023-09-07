using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCount { get; set; }

        public virtual ICollection<Sell> Sells { get; set; }

        public Product(string name,decimal price,int count)
        {
            ProductName=name;
            ProductPrice=price;
            ProductCount=count;
        }
        public Product()
        {

        }
        
        public override string ToString()
        {
            return $"{ProductName} - {ProductPrice} rub";
        }
        public override int GetHashCode()
        {
            return ProductId;
        }
        public override bool Equals(object? obj)
        {
            if(obj is Product product)
            {
                return ProductId.Equals(product.ProductId);
            }
            else
            {
                return false;
            } 
        }
    }
}
