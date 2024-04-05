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

        public event EventHandler<PriceChangedEventArgs>? PriceChanged;
        public StoragePresentation StoragePresentation { get; private set; }
        public Model(LogicAbstractApi? iLogicLayer)
        {
            this.iLogicLayer = iLogicLayer == null ? LogicAbstractApi.Create() : iLogicLayer;
            StoragePresentation = new StoragePresentation(this.iLogicLayer.Shop);
            ShoppingCart = new ShoppingCart(new ObservableCollection<IItemPresentation>(), this.iLogicLayer.Shop);
            MainViewVisibility = "Visible";
            CartViewVisibility = "Hidden";
            this.iLogicLayer.Shop.PriceChanged += OnPriceChanged;
        }

        public void OnPriceChanged(object sender, Logic.PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, new PriceChangedEventArgs(e.Id, e.Price));
        }

    }
}
