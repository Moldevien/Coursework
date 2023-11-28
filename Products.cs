using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Coursework
{
    public class Products : INotifyPropertyChanged
    {
        private ObservableCollection<Product> products;

        public ObservableCollection<Product> PRoducts
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged(nameof(PRoducts));
                OnPropertyChanged(nameof(TotalQuantity));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }
        public Products()
        {
            products = new ObservableCollection<Product>();

            string json = File.ReadAllText("ProductsList.json");
            if (!string.IsNullOrEmpty(json))
            {
                var deserialized = JsonSerializer.Deserialize<ObservableCollection<Product>>(json);
                if (deserialized != null)
                    foreach (var product in deserialized)
                        products.Add(product);
            }
        }
        public Products(ObservableCollection<Product> products) => this.products = products;
        public void AddProduct(Product product) { products.Add(product); SaveChanges(); }
        public void RemoveProduct(Product product) { products.Remove(product); SaveChanges(); }
        public void EditProduct(Product product, int index) { products.Insert(index, product); SaveChanges(); }
        public void SaveChanges()
        {
            File.WriteAllText("ProductsList.json", string.Empty);
            File.WriteAllText("ProductsList.json", JsonSerializer.Serialize(products.ToList(), new JsonSerializerOptions { WriteIndented = true }));
        }
        public string TotalQuantity { get { return $"Загальна кількість товару: {PRoducts.Sum(product => product.Quantity)} шт."; } }
        public string TotalPrice { get { return $"Загальна сума товару: {PRoducts.Sum(product => product.Quantity * product.Price)} грн."; } }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "") { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop)); }
    }
}