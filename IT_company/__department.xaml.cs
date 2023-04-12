using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;
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
    /// Логика взаимодействия для __department.xaml
    /// </summary>
    public partial class __department : Page
    {
        departmentTableAdapter otdel = new departmentTableAdapter();
        public __department()
        {
            InitializeComponent();
            DepartmentDataGrid.ItemsSource = otdel.GetData();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[а-яА-Я0-9\s]*$";
            if (Regex.IsMatch(DepartmentTextBox.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте кириллицу и цифры.";
            }
            else if (DepartmentTextBox.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                otdel.AddDepartment(DepartmentTextBox.Text);
                DepartmentDataGrid.ItemsSource = otdel.GetData();
                ErrorText.Text = ""; DepartmentTextBox.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[а-яА-Я0-9\s]*$";
            if (DepartmentDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (Regex.IsMatch(DepartmentTextBox.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте кириллицу и цифры.";
            }
            else if (DepartmentTextBox.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                otdel.UpdateDepartment(DepartmentTextBox.Text, Convert.ToInt32(department_id));
                DepartmentDataGrid.ItemsSource = otdel.GetData();
                ErrorText.Text = ""; DepartmentTextBox.Text = "";
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                otdel.DeleteDepartment(Convert.ToInt32(department_id));
                DepartmentDataGrid.ItemsSource = otdel.GetData();
                ErrorText.Text = ""; DepartmentTextBox.Text = "";
            }

        }
        public object department_id;
        private void DepartmentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentDataGrid.SelectedItem != null)
            {
                department_id = (DepartmentDataGrid.SelectedItem as DataRowView).Row[0];
                DepartmentTextBox.Text = (string)(DepartmentDataGrid.SelectedItem as DataRowView).Row[1];
            }
        }
    }
}
