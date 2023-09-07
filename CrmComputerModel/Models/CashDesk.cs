using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class CashDesk
    {
        public Crmcontext db;
        public int Number { get; set; }
        
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLength { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        
        public int count => Queue.Count;
        public event EventHandler<Check> CheckClosed;
        public override string ToString()
        {
            return $"CashDesk\nNumber {Number}";
        }
        public CashDesk(int number, Seller seller,Crmcontext db)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true ;
            MaxQueueLength = 10;
            ExitCustomer = 0;
            this.db=db ?? new Crmcontext();
        }
        public void Enqueue(Cart cart)
        {

            if (Queue.Count <= MaxQueueLength)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }
        public decimal Dequeue()
        {
            decimal Sum=0;
            if (Queue.Count == 0) return 0;
            var card = Queue.Dequeue();
            if (card != null)
            {
                var check = new Check()
                {
                    SellerID = Seller.SellerId,
                    Seller = Seller,
                    CustomerId = card.Customer.CustomerId,
                    Customer = card.Customer,
                    Date = DateTime.Now

                };
                if (!IsModel)
                {
                    db.Checks.Add(check);
                    db.SaveChanges();
                }
                else
                {
                    check.CheckId = 0;
                }
                var sells = new List<Sell>();
                foreach (Product product in card)
                {
                    if (product.ProductCount > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckId = check.CheckId,
                            Check = check,
                            ProductId = product.ProductId,
                            Product = product,
                        };
                        sells.Add(sell);

                        if (!IsModel)
                        {
                            db.Sells.Add(sell);
                            db.SaveChanges();
                        }
                        product.ProductCount--;
                        Sum += product.ProductPrice;
                       
                    }
                }
                check.Price = Sum;
                if (!IsModel)
                {
                    db.SaveChanges();
                }
                CheckClosed?.Invoke(this, check);
            }
            return Sum;
        }

    }
}
