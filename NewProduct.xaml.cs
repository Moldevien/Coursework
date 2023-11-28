using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public partial class NewProduct : Window
    {
        Products Products;
        Product selectedProduct;

        public NewProduct(ref Products products)
        {
            InitializeComponent();
            Products = products;
            DataContext = Products;
        }
        public NewProduct(ref Products products, Product SelectedProduct)
        {
            InitializeComponent();
            Add.Visibility = Visibility.Hidden;
            Edit.Visibility = Visibility.Visible;
            Products = products;
            selectedProduct = SelectedProduct;
            DataContext = SelectedProduct;
            Type.Text = SelectedProduct.Type;
            Name.Text = SelectedProduct.Name;
            Quantity.Text = SelectedProduct.Quantity.ToString();
            Date.Text = SelectedProduct.Date.ToString();
            Price.Text = SelectedProduct.Price.ToString();
        }
        private void AddNewProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Type.Text != string.Empty && 
                    Name.Text != string.Empty &&
                    Quantity.Text != string.Empty &&
                    Date.Text != string.Empty &&
                    Price.Text != string.Empty)
                {
                    Products.AddProduct(
                        new Product(
                            Type.Text,
                            Name.Text,
                            int.Parse(Quantity.Text),
                            DateTime.Parse(Date.Text),
                            decimal.Parse(Price.Text)));
                    Close();
                }
                else MessageBox.Show("Щось було не вказано");
            }
            catch { MessageBox.Show("Якесь поле було неправильно вказано"); }
        }
        private void EditProduct(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Type.Text != string.Empty &&
                    Name.Text != string.Empty &&
                    Quantity.Text != string.Empty &&
                    Date.Text != string.Empty &&
                    Price.Text != string.Empty)
                {
                    int index = 0;
                    for (int i = 0; i < Products.PRoducts.Count; i++)
                    {
                        if (Products.PRoducts[i] == selectedProduct)
                        {
                            index = i;
                            break;
                        }
                    }
                    Products.RemoveProduct(selectedProduct);
                    Products.EditProduct(
                        new Product(
                            Type.Text,
                            Name.Text,
                            int.Parse(Quantity.Text),
                            DateTime.Parse(Date.Text),
                            decimal.Parse(Price.Text)),
                    index);
                    Close();
                }
                else MessageBox.Show("Щось було не вказано");
            }
            catch { MessageBox.Show("Якесь поле було неправильно вказано"); }
        }
    }
}