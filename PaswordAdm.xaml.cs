using System;
using System.Collections.Generic;
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
    public partial class PaswordAdm : Window
    {
        Products products;
        public PaswordAdm(ref Products Products)
        {
            InitializeComponent();
            products = Products;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pasword.Password != "123")
            {
                MessageBox.Show("Введени пароль був не вірним");
                pasword.Password = "";
            }
            else
            {
                AdmPanel admPanel = new AdmPanel(ref products);
                Hide();
                admPanel.ShowDialog();
                Close();
            }
        }
    }
}
