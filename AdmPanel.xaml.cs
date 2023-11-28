using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework
{
    public partial class AdmPanel : Window
    {
        Products products;

        public AdmPanel(ref Products Products)
        {
            InitializeComponent();
            products = Products;
            DataContext = products;
        }
        private void ClearAll(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Ви впевнені що хочете очистити все?", "Clear", MessageBoxButton.YesNo)
                == MessageBoxResult.Yes) products.PRoducts.Clear();
            File.WriteAllText("Products.json", string.Empty);
            products.SaveChanges();
        }
        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            NewProduct newProduct = new NewProduct(ref products);
            newProduct.ShowDialog();
            products.SaveChanges();
        }
        private void RemoveProduct(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                if (MessageBox.Show("Ви впевнені що хочете видалити учасника?", "Remove", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    products.RemoveProduct((Product)ProductsList.SelectedItem);
                products.SaveChanges();
            }
            else MessageBox.Show("Не вибрано учасника.");
        }
        private void EditProduct(object sender, RoutedEventArgs e)
        {
            if (ProductsList.SelectedItem != null)
            {
                NewProduct newProduct = new NewProduct(ref products, (Product)ProductsList.SelectedItem);
                newProduct.ShowDialog();
                products.SaveChanges();
            }
            else MessageBox.Show("Не вибрано учасника.");
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            products.SaveChanges();
            Close();
        }
        private void RadioBoxChecked(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
                ProductsList.ItemsSource = products.PRoducts.Where(p => (DateTime.Now - p.Date).TotalDays > 30);
        }
        private void RadioBoxUnchecked(object sender, RoutedEventArgs e)
            { if (((CheckBox)sender).IsChecked == false) ProductsList.ItemsSource = products.PRoducts; }
    }
}