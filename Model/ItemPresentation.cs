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
    public class ItemPresentation : INotifyPropertyChanged
    {
        public ItemPresentation(string name, float price, Guid id, string itemType)
        {
            Name = name;
            Price = price;
            Id = id;
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
}
