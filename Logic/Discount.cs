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
        private float DiscountValue { get; set; }

        private IStorage Storage { get; set; }

        private Guid ItemOnSaleId { get; set; }
        private System.Timers.Timer ItemOnSaleTimer { get; }
        private Random Rand { get; set; }


        public Discount(IStorage storage)
        {
            Storage = storage;
            ItemOnSaleTimer = new System.Timers.Timer(1410);
            ItemOnSaleTimer.Elapsed += GetNewDiscount;
            ItemOnSaleTimer.AutoReset = true;
            ItemOnSaleTimer.Enabled = true;
            Rand = new Random();

        }

        public Tuple<Guid, float> GetDiscount()
        {
            return new Tuple<Guid, float>(ItemOnSaleId, DiscountValue);
        }

        private void GetNewDiscount(Object source, ElapsedEventArgs e)
        {
            if (Storage.ItemList.Count >= 1)
            {
                DiscountValue = ((float)Rand.NextDouble() * 0.5f) + 0.7f;
                IItem item = Storage.ItemList[Rand.Next(0, Storage.ItemList.Count)];
                ItemOnSaleId = item.id;
                Storage.ChangePrice(ItemOnSaleId, item.price * DiscountValue);
            }
        }
    }
}
