using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using System.Windows.Input;
using System.Diagnostics;
using ViewModel;

namespace ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        
        private Model.Model model;
        private ObservableCollection<ItemPresentation> items;
        private string mainViewVisibility;
        private string cartViewVisibility;
        private ShoppingCart shoppingCart;
        private float cartValue;
        private string connectButtonText;

        public ViewModelBase()
        {
            this.model = new Model.Model(null);
            this.model.PriceChanged += HandlePriceChanged;
            this.items = new ObservableCollection<ItemPresentation>();
            MainViewVisibility = this.model.MainViewVisibility;
            CartViewVisibility = this.model.CartViewVisibility;
            shoppingCart = this.model.ShoppingCart;
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                items.Add(item);
            }

            model.StoragePresentation.ItemChanged += OnItemChanged;
            model.StoragePresentation.ItemRemoved += OnItemRemoved;
            model.StoragePresentation.TransactionSucceeded += OnTransactionSucceeded;

            AppleButtonClick = new RelayCommand(AppleButtonClickHandler);
            CarrotButtonClick = new RelayCommand(CarrotButtonClickHandler);
            PearButtonClick = new RelayCommand(PearButtonClickHandler);
            CucumberButtonClick = new RelayCommand(CucumberButtonClickHandler);
            BananaButtonClick = new RelayCommand(BananaButtonClickHandler);
            CartButtonClick = new RelayCommand(CartButtonClickHandler);
            MainPageButtonClick = new RelayCommand(MainPageButtonClickHandler);
            BuyButtonClick = new RelayCommand(BuyButtonClickHandler);
            AllItemsButtonClick = new RelayCommand(AllItemsButtonClickHandler);
            ItemButtonClick = new ParameterCommand<Guid>(ItemButtonClickHandler);
            ConnectButtonClick = new RelayCommand(() => ConnectButtonClickHandler());
        }

        private void OnTransactionSucceeded(object? sender, List<ItemPresentation> e)
        {
            
        }

        public ObservableCollection<ItemPresentation> Items
        {
            get
            {
                return items;
            }
            set
            {
                if (value.Equals(items))
                    return;
                items = value;
                OnPropertyChanged("Items");
            }

        }


        public ICommand AppleButtonClick { get; set; }
        public ICommand CarrotButtonClick { get; set; }
        public ICommand PearButtonClick { get; set; }
        public ICommand CucumberButtonClick { get; set; }
        public ICommand BananaButtonClick { get; set; }

        public ICommand CartButtonClick { get; set; }
        public ICommand ItemButtonClick { get; set; }
        public ICommand MainPageButtonClick { get; set; }
        public ICommand BuyButtonClick { get; set; }

        public ICommand AllItemsButtonClick { get; set; }

        public ICommand ConnectButtonClick { get; set; }

        private async Task ConnectButtonClickHandler()
        {
            if (!model.StoragePresentation.IsConnected())
            {
                ConnectButtonText = "Connecting...";
                bool result = await model.StoragePresentation.Connect(new Uri("ws://localhost:8081"));

                if (result)
                {
                    ConnectButtonText = "Connected!";
                    Items.Clear();
                    foreach (ItemPresentation item in model.StoragePresentation.GetItems())
                        Items.Add(item);
                }
            }
            else
            {
                await model.StoragePresentation.Disconnect();
                ConnectButtonText = "Disconnected!";
                Items.Clear();
            }
        }


        private void OnItemChanged(object? sender, ItemPresentation e)
        {
            ObservableCollection<ItemPresentation> newItems = new ObservableCollection<ItemPresentation>(Items);
            ItemPresentation item = newItems.FirstOrDefault(x => x.Id == e.Id);

            if (item != null)
            {
                int itemIndex = newItems.IndexOf(item);

                if (e.Type.ToLower() == "deleted")
                    newItems.RemoveAt(itemIndex);
                else
                {
                    newItems[itemIndex].Name = e.Name;
                    newItems[itemIndex].Price = e.Price;
                    newItems[itemIndex].Id = e.Id;
                    newItems[itemIndex].Type = e.Type;
                }
            }
            else
                newItems.Add(e);

            Items = new ObservableCollection<ItemPresentation>(newItems);
        }

        private void OnItemRemoved(object? sender, ItemPresentation e)
        {
            ObservableCollection<ItemPresentation> newItems = new ObservableCollection<ItemPresentation>(Items);
            ItemPresentation Item = newItems.FirstOrDefault(x => x.Id == e.Id);

            if (Item != null)
            {
                int itemIndex = newItems.IndexOf(Item);
                newItems.RemoveAt(itemIndex);
            }

            Items = new ObservableCollection<ItemPresentation>(newItems);
        }

        private void CartButtonClickHandler()
        {
            CartViewVisibility = "Visible";
            MainViewVisibility = "Hidden";

        }
        private void MainPageButtonClickHandler()
        {   
            CartViewVisibility = "Hidden";
            MainViewVisibility = "Visible";

            Items.Clear();
            foreach (ItemPresentation item in model.StoragePresentation.GetItems())
            {
                Items.Add(item);
            }
        }

        private void AllItemsButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                items.Add(item);
            }
        }

        private void AppleButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Type.Equals("Apple"))
                    items.Add(item);
            }
        }
        private void CarrotButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Type.Equals("Carrot"))
                    items.Add(item);
            }

        }
           
        
        private void PearButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Type.Equals("Pear"))
                    items.Add(item);
            }
        }
        private void CucumberButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Type.Equals("Cucumber"))
                    items.Add(item);
            }
        }
        private void BananaButtonClickHandler()
        {
            items.Clear();
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Type.Equals("Banana"))
                    items.Add(item);
            }
        }


        public void ItemButtonClickHandler(Guid id)
        {
            foreach (ItemPresentation item in this.shoppingCart.Items)
            {
                if (item.Id.Equals(id))
                {
                    return;
                }
            }
            foreach (ItemPresentation item in this.model.StoragePresentation.GetItems())
            {
                if (item.Id.Equals(id))
                {
                    shoppingCart.Add(item);
                    CartValue = shoppingCart.Sum();
                }
            }
        }

        public void BuyButtonClickHandler()
        {
            ShoppingCart.Buy();
            CartValue = shoppingCart.Sum();

            Items.Clear();
            foreach (ItemPresentation item in model.StoragePresentation.GetItems())
            {
                Items.Add(item);
            }
        }

        public ShoppingCart ShoppingCart
        {
            get
            {
                return shoppingCart;
            }
            set
            {
                if (value.Equals(shoppingCart))
                    return;
                shoppingCart = value;
                OnPropertyChanged("ShoppingCart");
            }
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
                OnPropertyChanged("MainViewVisibility");
            }
        }
        public string CartViewVisibility
        {
            get
            {
                return cartViewVisibility;
            }
            set
            {
                if (value.Equals(cartViewVisibility))
                    return;
                cartViewVisibility = value;
                OnPropertyChanged("CartViewVisibility");
            }
        }
        public float CartValue
        {
            get
            {
                return cartValue;
            }
            set
            {
                if (value.Equals(cartValue))
                    return;
                cartValue = value;
                OnPropertyChanged("CartValue");
            }
        }

        public string ConnectButtonText
        {
            get { return connectButtonText; }
            set
            {
                if (value.Equals(connectButtonText))
                    return;
                connectButtonText = value;
                OnPropertyChanged("ConnectButtonText");
            }
        }

        public void RefreshItems()
        {
            Items.Clear();
            foreach (ItemPresentation item in model.StoragePresentation.GetItems())
            {
                Items.Add(item);
            }
        }

        public void HandlePriceChanged(object? sender, Model.PriceChangedEventArgs args)
        {
            //RefreshItems();

            ObservableCollection<ItemPresentation> newItems = Items;
            ItemPresentation item = newItems.FirstOrDefault(x => x.Id == args.Id);
            int itemIndex = newItems.IndexOf(item);
            newItems[itemIndex].Price = args.Price;
            Items = new ObservableCollection<ItemPresentation>(newItems);

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}