using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Logic
{
    internal class Discount : IDiscount
    {
        private Guid ItemOnSaleId { get; set; }
        private float DiscountValue { get; set; }

        private IStorage Storage { get; set; }
        private Random Rand { get; set; }

        private bool canDiscount;
        private object objectLock = new object();


        public Discount(IStorage storage)
        {
            Storage = storage;
            Rand = new Random();
            GetNewDiscount();
            canDiscount = true;
        }

        ~Discount()
        {
            canDiscount = false;
        }

        public Tuple<Guid, float> GetDiscount()
        {
            return new Tuple<Guid, float>(ItemOnSaleId, DiscountValue);
        }

        private async void GetNewDiscount()
        {
            while (true)
            {
                IItem item;
                float waitSeconds = 7f;
                await Task.Delay((int)Math.Truncate(waitSeconds * 1000f));

                lock (objectLock)
                {
                    if (Storage.ItemList.Count > 0)
                    {
                        DiscountValue = ((float)Rand.NextDouble() * 0.7f) + 0.5f;
                        item = Storage.ItemList[Rand.Next(0, Storage.ItemList.Count)];
                        ItemOnSaleId = item.Id;
                        Storage.ChangePrice(ItemOnSaleId, item.Price * DiscountValue);
                    }
                }

                lock (objectLock)
                {
                    if (!canDiscount)
                    {
                        break;
                    }
                }

            }
        }
    }
}
