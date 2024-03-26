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
        private LogicAbstractApi iLogicLayer;
        public string MainViewVisibility;
        public string CartViewVisibility;
        public ShoppingCart ShoppingCart;

        public StoragePresentation StoragePresentation { get; private set; }
        public Model(LogicAbstractApi? iLogicLayer)
        {
            this.iLogicLayer = iLogicLayer == null ? LogicAbstractApi.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            ShoppingCart = new ShoppingCart(new ObservableCollection<ItemPresentation>(), this.iLogicLayer.Shop);
            Debug.WriteLine("Here");
            MainViewVisibility = "Visiblie";
            CartViewVisibility = "Hidden";
        }

    }
}
