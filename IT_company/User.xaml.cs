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

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
            PageFrame.Content = new __buyer();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __buyer();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __type_buyer();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new _payment_type();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __type_service();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __price_list();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __purchases();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new __history();
        }
    }
}
