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
using IT_company.ITDataSetTableAdapters;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        role_sTableAdapter role = new role_sTableAdapter();   
        public Admin()
        {
            InitializeComponent();
            PageFrame.Content = new ___staffs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __roles();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __auth();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new ___staffs();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __department();
        }
    }
}
