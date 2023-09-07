using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class Generator
    {
        Random random=new Random();
        public List<CashDesk> CashDesks { get; set; }=new List<CashDesk>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; } = new List<Product>();  
        public List<Seller> Sellers { get; set; }=new List<Seller>();
        public List<Customer> GetCustomers(int count)
        {
            var result = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                var customer = new Customer()
                {
                    CustomerId = Customers.Count,
                    CustomerName = GetRandomText()
                };
                Customers.Add(customer);
                result.Add(customer);
            }
            return result;
        }
        public List<Seller> GetSellers(int count)
        {
            var result = new List<Seller>();
            for (int i = 0; i < count; i++) 
            {
                var seller = new Seller()
                {
                    SellerId = Sellers.Count,
                    SellerName = GetRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        public List<Product> GetProducts(int count)
        {
            var result = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var product = new Product()
                {
                    ProductId = Products.Count,
                    ProductName = GetRandomText(),
                    ProductCount=random.Next(5,200),
                    ProductPrice=Convert.ToDecimal(random.NextDouble()+random.Next(10,1000))
                };
                Products.Add(product);
                result.Add(product);
            }
            return result;
        }
        public List<Product> GetRandomProducts(int min, int max)
        {
            var result = new List<Product>();
            var count = random.Next(min, max);
            for (int i = 0; i < count; i++)
            {
                result.Add(Products[random.Next(Products.Count - 1)]);
            }
            return result;
        }

        private static string GetRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
    

}
