using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;

namespace Model
{
    public class Model
    {
        private ILogicLayer iLogicLayer;
        public string MainViewVisibility;
        public string CartViewVisibility;
        public ShoppingCart ShoppingCart;

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(ILogicLayer? iLogicLayer)
        {
            this.iLogicLayer = iLogicLayer == null ? ILogicLayer.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            ShoppingCart = new ShoppingCart(new ObservableCollection<ItemPresentation>(), this.iLogicLayer.Shop);
            Debug.WriteLine("Here");
            MainViewVisibility = "Visiblie";
            CartViewVisibility = "Hidden";
        }

    }
}
