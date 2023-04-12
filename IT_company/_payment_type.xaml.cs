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

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для _payment_type.xaml
    /// </summary>
    public partial class _payment_type : Page
    {
        payment_typeTableAdapter type = new payment_typeTableAdapter();
        public _payment_type()
        {
            InitializeComponent();
            TypesDataGrid.ItemsSource = type.GetData(); 
        }
        public object type_id;
        private void TypesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypesDataGrid.SelectedItem != null)
            {
                type_id = (TypesDataGrid.SelectedItem as DataRowView).Row[0];
                Type.Text = (string)(TypesDataGrid.SelectedItem as DataRowView).Row[1];

            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[а-яА-Я\s]*$";
            if (Regex.IsMatch(Type.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте кириллицу.";
            }
            else if (Type.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                type.AddType(Type.Text);
                TypesDataGrid.ItemsSource = type.GetData();
                ErrorText.Text = ""; Type.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"^[а-яА-Я\s]*$";
            if (TypesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (Regex.IsMatch(Type.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте кириллицу.";
            }
            else if (Type.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                type.UpdateType(Type.Text, Convert.ToInt32(type_id));
                TypesDataGrid.ItemsSource = type.GetData();
                ErrorText.Text = ""; Type.Text = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TypesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                type.DeleteType(Convert.ToInt32(type_id));
                TypesDataGrid.ItemsSource = type.GetData();
                ErrorText.Text = ""; Type.Text = "";
            }
        }
    }
}
