using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using static IT_company.ITDataSet;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для __auth.xaml
    /// </summary>
    public partial class __auth : Page
    {
        authTableAdapter auth = new authTableAdapter();
        staffTableAdapter staff = new staffTableAdapter();
        public __auth()
        {
            InitializeComponent();
            AuthDataGrid.ItemsSource = auth.GetData();
            StaffBox.ItemsSource = staff.GetData();
            StaffBox.DisplayMemberPath = "name_staff";
        }
        public object auth_id;
        private void AuthDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthDataGrid.SelectedItem != null)
            {
                auth_id = (AuthDataGrid.SelectedItem as DataRowView).Row[0];
                Login.Text = (string)(AuthDataGrid.SelectedItem as DataRowView).Row[1];
                Password.Password = (string)(AuthDataGrid.SelectedItem as DataRowView).Row[2];
                int staffId = Convert.ToInt32((AuthDataGrid.SelectedItem as DataRowView).Row[3]);
                var allStaff = staff.GetData().Rows;
                for (int i = 0; i < allStaff.Count; i++)
                {
                    if (Convert.ToInt32(allStaff[i][0]) == staffId)
                    {
                        StaffBox.Text = allStaff[i][2].ToString();
                    }
                }
            }
        }
        public object staff_id;
        private void StaffBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffBox.SelectedItem != null)
            {
                staff_id = (StaffBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9]*$";
            string pattern2 = @"^[a-zA-Z0-9\D]*$";
            if (Regex.IsMatch(Login.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте латиницу и цифры без  спецсимволов.";
            }
            else if (Regex.IsMatch(Password.Password, pattern2, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте латиницу, цифры и спецсимволы.";
            }
            else if (Login.Text == "" | Password.Password == "" | StaffBox == null)
            {
                ErrorText.Text = "Не все поля заполнены";
            }
            else
            {
                auth.AddAuth(Login.Text, Password.Password, Convert.ToInt32(staff_id));
                AuthDataGrid.ItemsSource = auth.GetData();
                Login.Text = ""; Password.Password = ""; StaffBox.Text = ""; ErrorText.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[a-zA-Z0-9]*$";
            string pattern2 = @"^[a-zA-Z0-9\D]*$";
            if (AuthDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (Regex.IsMatch(Login.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте латиницу и цифры без  спецсимволов.";
            }
            else if (Regex.IsMatch(Password.Password, pattern2, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте латиницу, цифры и спецсимволы.";
            }
            else if (Login.Text == "" | Password.Password == "" | StaffBox == null)
            {
                ErrorText.Text = "Не все поля заполнены";
            }
            else
            {
                auth.UpdateAuth(Login.Text, Password.Password, Convert.ToInt32(staff_id), Convert.ToInt32(auth_id));
                AuthDataGrid.ItemsSource = auth.GetData();
                Login.Text = ""; Password.Password = ""; StaffBox.Text = ""; ErrorText.Text = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (AuthDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                auth.DeleteAuth(Convert.ToInt32(auth_id));
                Login.Text = ""; Password.Password = ""; StaffBox.Text = ""; ErrorText.Text = "";
            }
        }
    }
}
