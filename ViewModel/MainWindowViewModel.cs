﻿using System;
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


        #region private


        private ShoppingCart shoppingCart;
        private ObservableCollection<ItemPresentation> items;
        private Timer timer;
        private ModelAbstractApi ModelLayer;
        private string mainViewVisibility;

        #endregion private
    }
}
