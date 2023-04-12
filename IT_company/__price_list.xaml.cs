using ControlzEx.Standard;
using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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
using static IT_company.ITDataSet;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для __price_list.xaml
    /// </summary>
    public partial class __price_list : Page
    {
        price_listTableAdapter price = new price_listTableAdapter();
        type_serviceTableAdapter type = new type_serviceTableAdapter();
        public __price_list()
        {
            InitializeComponent();
            ListDataGrid.ItemsSource = price.GetData();
            TypeBox.ItemsSource = type.GetData();
            TypeBox.DisplayMemberPath = "type_service";
        }
        public object price_id;
        private void ListDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListDataGrid.SelectedItem != null)
            {
                price_id = (ListDataGrid.SelectedItem as DataRowView).Row[0];
                Name.Text = (string)(ListDataGrid.SelectedItem as DataRowView).Row[1];
                Price.Text = Convert.ToString((ListDataGrid.SelectedItem as DataRowView).Row[2]);
                int typeId = Convert.ToInt32((ListDataGrid.SelectedItem as DataRowView).Row[3]);
                var allTypes = type.GetData().Rows;
                for (int i = 0; i < allTypes.Count; i++)
                {
                    if (Convert.ToInt32(allTypes[i][0]) == typeId)
                    {
                        TypeBox.Text = allTypes[i][1].ToString();
                    }
                }
            }
        }
        public object types;
        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeBox.SelectedItem != null)
            {
                types = (TypeBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var stringNumber = Price.Text;
            bool isNumber = int.TryParse(stringNumber, out int numericValue);
            string pattern = @"^[а-яА-Яa-zA-Z0-9\s]*$";
            if (Regex.IsMatch(Name.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте кириллицу, латиницу и цифры.";
            }
            else if (TypeBox.SelectedItem == null | Name.Text == ""
                | Price.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else if (isNumber != true)
            {
                ErrorText.Text = "В строке с ценой только целочисленные значения!";
            }
            else if (isNumber == true && numericValue < 0)
            {
                ErrorText.Text = "Цена не может быть отрицательной!";
            }
            else
            {
                price.AddList(Name.Text, numericValue, Convert.ToInt32(types));
                ListDataGrid.ItemsSource = price.GetData();
                Name.Text = ""; Price.Text = ""; TypeBox.Text = "";ErrorText.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var stringNumber = Price.Text;
            bool isNumber = int.TryParse(stringNumber, out int numericValue);
            string pattern = @"^[а-яА-Яa-zA-Z0-9\s]*$";
            if (ListDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку";
            }
            
            else if (Regex.IsMatch(Name.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                 ErrorText.Text = "Используйте кириллицу, латиницу и цифры.";
            }
            else if (TypeBox.SelectedItem == null | Name.Text == ""
                | Price.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else if (isNumber != true)
            {
                ErrorText.Text = "В строке с ценой только целочисленные значения!";
            }
            else if (isNumber == true && numericValue < 0)
            {
                ErrorText.Text = "Цена не может быть отрицательной!";
            }
            else
            {
                price.UpdateList(Name.Text, numericValue, Convert.ToInt32(types), Convert.ToInt32(price_id));
                ListDataGrid.ItemsSource = price.GetData();
                Name.Text = ""; Price.Text = ""; TypeBox.Text = ""; ErrorText.Text = "";
             }
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                price.DeleteList(Convert.ToInt32(price_id));
                ListDataGrid.ItemsSource = price.GetData();
                Name.Text = ""; Price.Text = ""; TypeBox.Text = ""; ErrorText.Text = "";
            }
        }
    }
}
