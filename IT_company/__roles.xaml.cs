using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
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

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для __roles.xaml
    /// </summary>
    public partial class __roles : Page
    {
        role_sTableAdapter roless = new role_sTableAdapter();
        public __roles()
        {
            InitializeComponent();
            RolesDataGrid.ItemsSource = roless.GetData();
        }
        public object role_id;

        private void RolesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RolesDataGrid.SelectedItem != null)
            {
                role_id = (RolesDataGrid.SelectedItem as DataRowView).Row[0];
                Role.Text = (string)(RolesDataGrid.SelectedItem as DataRowView).Row[1];

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9\s]*$";
            if (Regex.IsMatch(Role.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text  = "Используйте английские буквы и цифры.";
            }
            else if (Role.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                roless.AddRole(Role.Text);
                RolesDataGrid.ItemsSource = roless.GetData();
                ErrorText.Text = ""; Role.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9\s]*$";
            if (RolesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (Regex.IsMatch(Role.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте английские буквы и цифры.";
            }
            else if (Role.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                roless.UpdateRole(Role.Text, Convert.ToInt32(role_id));
                RolesDataGrid.ItemsSource = roless.GetData();
                ErrorText.Text = ""; Role.Text = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (RolesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                roless.DeleteRole(Convert.ToInt32(role_id));
                RolesDataGrid.ItemsSource = roless.GetData();
                ErrorText.Text = ""; Role.Text = "";
            }
        }
    }
}
