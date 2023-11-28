using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework
{
    public partial class MainWindow : Window
    {
        public Products products;
        public Dictionary<Product, int> BuyProducts;

        public MainWindow()
        {
            InitializeComponent();
            products = new Products();
            BuyProducts = new Dictionary<Product, int>();
            DataContext = products;
            All.IsChecked = true;
        }

        private void Administration(object sender, RoutedEventArgs e)
        {
            PaswordAdm passwordAdm = new PaswordAdm(ref products);
            passwordAdm.ShowDialog();
            UpdateProductsList(sender);
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                var selected = (Product)ProductsList.SelectedItem;
                if (BuyProducts.ContainsKey(selected))
                    BuyProducts[selected]++;
                else BuyProducts[selected] = 1;
            }
            else MessageBox.Show("Не вибрано продукт.");
            BuyList.ItemsSource = BuyProducts.ToList();
        }

        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (BuyList.SelectedItem != null)
            {
                var selected = ((KeyValuePair<Product, int>)BuyList.SelectedItem).Key;
                if (BuyProducts.ContainsKey(selected))
                    if (BuyProducts[selected] > 1)
                        BuyProducts[selected]--;
                    else BuyProducts.Remove(selected);
                else MessageBox.Show("Не вибрано продукт.");
            }
            else MessageBox.Show("Не вибрано продукт.");
            BuyList.ItemsSource = BuyProducts.ToList();
        }

        private void GenerateReceipt_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder Text = new StringBuilder();
            if (BuyProducts.Count != 0)
            {
                decimal Sum = 0;
                Text.AppendLine("Чек на покупку:");
                foreach (var entry in BuyProducts)
                {
                    var available = products.PRoducts.FirstOrDefault(p => p.Equals(entry.Key));
                    if (available != null && available.Quantity >= entry.Value)
                    {
                        Text.AppendLine($"{entry.Key.Name} - {entry.Value} шт. * {entry.Key.Price:C} = {entry.Value * entry.Key.Price:C}");
                        Sum += entry.Value * entry.Key.Price;
                        available.Quantity -= entry.Value;
                    }
                    else
                    {
                        MessageBox.Show($"Не вистачає товару: {entry.Key.Name}");
                        return;
                    }
                }
                Text.AppendLine($"Загальна сума: {Sum:C}");
                BuyProducts.Clear();
                MessageBox.Show(Text.ToString(), "Чек покупки");
            }
            else
            {
                Text.AppendLine("Чек на покупку: Пустий");
                MessageBox.Show(Text.ToString(), "Чек покупки");
            }

            BuyList.ItemsSource = BuyProducts.ToList();
            products.SaveChanges();
        }

        private void RadioBoxChecked(object sender, RoutedEventArgs e)
        {
            UpdateProductsList(sender);
        }

        private void UpdateProductsList(object sender)
        {
            if (All.IsChecked == true) ProductsList.ItemsSource = products.PRoducts.Where(p => p.Date.AddDays(30) >= DateTime.Now);
            else
            {
                ObservableCollection<Product> temp = new ObservableCollection<Product>();
                foreach (Product product in products.PRoducts.Where(p => p.Date.AddDays(30) >= DateTime.Now && p.Type == ((RadioButton)sender).Content.ToString()))
                    temp.Add(product);
                ProductsList.ItemsSource = temp;
            }
        }
    }

}