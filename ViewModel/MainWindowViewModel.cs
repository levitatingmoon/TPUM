using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;

namespace ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        #region public API
        public MainWindowViewModel() : this(ModelAbstractApi.CreateApi()) { }
        public MainWindowViewModel(ModelAbstractApi modelAbstractApi)
        {
            ModelLayer = modelAbstractApi;
            //Radious = ModelLayer.Radius;
            //ColorString = ModelLayer.ColorString;
            //MainViewVisibility = ModelLayer.MainViewVisibility;

        }

        public string MainViewVisibility
        {
            get
            {
                return mainViewVisibility;
            }
            set
            {
                if (value.Equals(mainViewVisibility))
                    return;
                mainViewVisibility = value;
                RaisePropertyChanged("MainViewVisibility");
            }
        }

        #endregion public API

        #region private


        private ShoppingCart shoppingCart;
        private ObservableCollection<ItemPresentation> items;
        private Timer timer;
        private ModelAbstractApi ModelLayer;
        private string mainViewVisibility;

        #endregion private
    }
}
