using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class ItemPresentation : IItemPresentation, INotifyPropertyChanged
    {
        public ItemPresentation(string name, float price, Guid id, string itemType)
        {
            Name = name;
            Price = price;
            Id = id;
            Type = itemType;
        }

        public ItemPresentation(string name, float price, string itemType)
        {
            Name = name;
            Price = price;
            Id = Guid.NewGuid();
            Type = itemType;
        }

        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IItemPresentation
    {


        public string Name { get; set; }
        public float Price { get; set; }
        public Guid Id { get; set; }
        public string Type { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

    }
}
