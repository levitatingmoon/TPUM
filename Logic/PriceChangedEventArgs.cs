using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class PriceChangedEventArgs : EventArgs
    {
        public Guid Id { get; }
        public float Price { get; }

        public PriceChangedEventArgs(Guid id, float price) {

            Id = id;
            Price = price; 
        }

    }
}
