using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmComputerModel.Models
{
    public class ShopComputerModel
    {
        Generator generator = new Generator();

        Random random = new Random();

        bool isWorking = false;
        public decimal Earn { get; set; }
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public int CustomerSpeed { get; set; } = 200;
        public int CashDeskSpeed { get; set; } = 200;
        public int CashDeskCount { get; set; } = 3;
        public bool RandomPick { get; set; }
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();

        public ShopComputerModel()
        {
            var sellers = generator.GetSellers(20);
            generator.GetProducts(200);
            generator.GetCustomers(20);
            foreach (var seller in sellers) 
            {
                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < CashDeskCount; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(),null));
            }
        }
        public ShopComputerModel(int desiredCount)
        {
            var sellers = generator.GetSellers(20);
            generator.GetProducts(200);
            generator.GetCustomers(20);
            CashDeskCount = desiredCount;
            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < CashDeskCount; i++)
            {
                CashDesks.Add(new CashDesk(i+1, Sellers.Dequeue(), null));
            }
        }

        public void Start()
        {
            isWorking = true;
            Task.Run(()=>CreateCarts(10));
            var cashDesksThreads = CashDesks.Select(c => new Task(() => CashDeskWork(c)));
            foreach(var task in cashDesksThreads)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            isWorking = false;
        }
        
        public void ChangeCashDeskCount(int desiredCount)
        {
            CashDeskCount = desiredCount;
        }
        private void CashDeskWork(CashDesk cashDesk)
        {
            while (isWorking)
            {
                if (cashDesk.count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(this.CashDeskSpeed);
                }
            }
        }

        private void CreateCarts(int customerCounts)
        {
            while (isWorking)
            {
                var customers = generator.GetCustomers(customerCounts);
                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach (var product in generator.GetRandomProducts(10, 35))
                    {
                        cart.AddProduct(product);
                    }
                    if (RandomPick)
                    {
                        var cashDesk = CashDesks[random.Next(CashDesks.Count)];
                        cashDesk.Enqueue(cart);
                    }
                    else
                    {
                        int min = CashDesks.Min(x => x.count);
                        var ShortestCashDesk = CashDesks.Where(x => x.count == min).Last();
                        ShortestCashDesk.Enqueue(cart);
                    }
                }
                Thread.Sleep (CustomerSpeed);
            }
        }
    }
}
