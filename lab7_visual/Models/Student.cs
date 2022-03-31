using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Media;

namespace lab7_visual.Models
{
    public class Student : INotifyPropertyChanged
    {
        double average = 0;
        string averageColor = "Red";
        List<string> itemsNames = new List<string>() { "Info", "Visual", "OOP" };
        List<Item> items = new List<Item>();
        ObservableCollection<Item> itemList;

        public Student(List<string> _itemsNames, string fio = "")
        {
            Fio = fio;
            if (_itemsNames.Count() != 0)
            {
                itemsNames = _itemsNames;
            }
            foreach (string itemName in itemsNames)
            {
                items.Add(new Item(itemName));
            }
            itemList = new ObservableCollection<Item>(items);
        }

        public Student(List<Item> _items, string fio = "", double _average = 0)
        {
            Fio = fio;
            Average = _average;
            items = _items;
            itemList = new ObservableCollection<Item>(_items);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsSelected { get; set; }
        public string Fio { get; set; }

        public ObservableCollection<Item> ItemList
        {
            get => itemList;
            set
            {
                itemList = value;
            }
        }

        public string AverageColor
        {
            get => averageColor;
            set
            {
                averageColor = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshAverage()
        {
            try
            {
                average = 0;
                foreach (Item item in items)
                {
                    Average += Convert.ToDouble(item.Score) / (double)items.Count();
                }
            }
            catch
            {
                Average = 0;
            }
        }

        public double Average
        {
            get => average;
            set
            {
                average = value;
                if (average < 1)
                    AverageColor = "Red";
                else if (average < 1.5)
                    AverageColor = "Yellow";
                else
                    AverageColor = "Green";
                NotifyPropertyChanged();
            }
        }
    }
}
