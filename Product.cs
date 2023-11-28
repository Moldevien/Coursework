using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Coursework
{
    public class Product: INotifyPropertyChanged
    {
        private string type;
        private string name;
        private int quantity;
        private DateTime date;
        private decimal price;

        public Product(string type, string name, int quantity, DateTime date, decimal price)
        {
            Type = type;
            Name = name;
            Date = date;
            Quantity = quantity;
            Price = price;
        }
        public Product() { }

        public string Type
        {
            get { return type; }
            set
            {
                if (value.Length < 10) type = value;
                else throw new ArgumentOutOfRangeException();
                OnPropertyChanged("Type");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                if (value.Length < 50) name = value;
                else throw new ArgumentOutOfRangeException();
                OnPropertyChanged("Name");
            }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                price = value;
                OnPropertyChanged("Price");
            }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        public DateTime Date
        {
            get { return date; }
            set
            {
                if (value.Year > 1921 && value.Year <= 2023 && value.Month > 0 && value.Month <= 12 && value.Day > 0 && value.Day <= 31) date = value;
                else throw new ArgumentOutOfRangeException();
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}
