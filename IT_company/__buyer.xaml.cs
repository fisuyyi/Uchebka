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
    /// Логика взаимодействия для __buyer.xaml
    /// </summary>
    public partial class __buyer : Page
    {
        buyerTableAdapter buyer = new buyerTableAdapter();
        buyer_typeTableAdapter types_buyer = new buyer_typeTableAdapter();
        public __buyer()
        {
            InitializeComponent();
            BuyerDataGrid.ItemsSource = buyer.GetData();
            TypeBox.ItemsSource = types_buyer.GetData();
            TypeBox.DisplayMemberPath = "type_nam";
        }

        public object buyer_id;
        private void BuyerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyerDataGrid.SelectedItem != null)
            {
                buyer_id = (BuyerDataGrid.SelectedItem as DataRowView).Row[0];
                Surname.Text = (string)(BuyerDataGrid.SelectedItem as DataRowView).Row[2];
                Name.Text = (string)(BuyerDataGrid.SelectedItem as DataRowView).Row[3];
                Middle.Text = (string)(BuyerDataGrid.SelectedItem as DataRowView).Row[4];
                Company.Text = Convert.ToString((BuyerDataGrid.SelectedItem as DataRowView).Row[5]);
                int type_id = Convert.ToInt32((BuyerDataGrid.SelectedItem as DataRowView).Row[1]);
                var allTypes = types_buyer.GetData().Rows;
                for (int i = 0; i < allTypes.Count; i++)
                {
                    if (Convert.ToInt32(allTypes[i][0]) == type_id)
                    {
                        TypeBox.Text = allTypes[i][1].ToString();
                    }
                }
            }
        }
        public object typp;
        private void TypeBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeBox.SelectedItem != null)
            {
                typp = (TypeBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            string pattern1 = @"^[A-Za-zа-яА-Я\s]*$";
            string pattern = @"^[а-яА-Я\s]*$";
            if (Regex.IsMatch(Surname.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте только кириллицу.";
            }
            else if (Regex.IsMatch(Name.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте только кириллицу.";
            }
            else if (Regex.IsMatch(Middle.Text, pattern, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте только кириллицу.";
            }
            /*else if (Regex.IsMatch(Company.Text, pattern1, RegexOptions.IgnoreCase) != true)
            {
                ErrorText.Text = "Используйте только буквы - Латиница или Кириллица.";
            }*/
            else if (TypeBox.SelectedItem == null | Surname.Text == "" | Name.Text == ""
                | Middle.Text == "")
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else if (Convert.ToInt32(typp) == 2)
            {
                buyer.AddBuyerWithoutCompany(Convert.ToInt32(typp), Surname.Text, Name.Text, Middle.Text);
                BuyerDataGrid.ItemsSource = buyer.GetData();
                Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Company.Text = ""; ErrorText.Text = ""; TypeBox.Text = "";
            }
            if (TypeBox.SelectedItem != null)
            {
                if (Convert.ToInt32(typp) == 1)
                {
                    if (Company.Text == "")
                    {
                        ErrorText.Text = "При выборе юридического лица необходимо заполнить поле с названием компании!";

                    }
                    else if (Regex.IsMatch(Company.Text, pattern1, RegexOptions.IgnoreCase) != true)
                    {
                        ErrorText.Text = "В строке с компанией используйте только буквы - Латиница или Кириллица.";
                    }
                    else
                    {
                        buyer.AddBuyer(Convert.ToInt32(typp), Surname.Text, Name.Text, Middle.Text, Company.Text);
                        BuyerDataGrid.ItemsSource = buyer.GetData();
                        Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Company.Text = ""; ErrorText.Text = ""; TypeBox.Text = "";
                    }
                }
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (BuyerDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else
            {
                string pattern1 = @"^[A-Za-zа-яА-Я\s]*$";
                string pattern = @"^[а-яА-Я\s]*$";

                if (Regex.IsMatch(Surname.Text, pattern, RegexOptions.IgnoreCase) != true)
                {
                    ErrorText.Text = "Используйте только кириллицу.";
                }
                else if (Regex.IsMatch(Name.Text, pattern, RegexOptions.IgnoreCase) != true)
                {
                    ErrorText.Text = "Используйте только кириллицу.";
                }
                else if (Regex.IsMatch(Middle.Text, pattern, RegexOptions.IgnoreCase) != true)
                {
                    ErrorText.Text = "Используйте только кириллицу.";
                }
                /*else if (Regex.IsMatch(Company.Text, pattern1, RegexOptions.IgnoreCase) != true)
                {
                    ErrorText.Text = "Используйте только буквы - Латиница или Кириллица.";
                }*/
                else if (TypeBox.SelectedItem == null | Surname.Text == "" | Name.Text == ""
                    | Middle.Text == "")
                {
                    ErrorText.Text = "Не все поля заполнены!";
                }
                else if (Convert.ToInt32(typp) == 2)
                {
                    buyer.UpdateBuyer(Convert.ToInt32(typp), Surname.Text, Name.Text, Middle.Text, "",Convert.ToInt32(buyer_id));
                    BuyerDataGrid.ItemsSource = buyer.GetData();
                    Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Company.Text = ""; ErrorText.Text = ""; TypeBox.Text = "";
                }
                if (TypeBox.SelectedItem != null)
                {
                    if (Convert.ToInt32(typp) == 1)
                    {
                        if (Company.Text == "")
                        {
                            ErrorText.Text = "При выборе юридического лица необходимо заполнить поле с названием компании!";

                        }
                        else if (Regex.IsMatch(Company.Text, pattern1, RegexOptions.IgnoreCase) != true)
                        {
                            ErrorText.Text = "В строке с компанией используйте только буквы - Латиница или Кириллица.";
                        }
                        else
                        {
                            buyer.UpdateBuyer(Convert.ToInt32(typp), Surname.Text, Name.Text, Middle.Text, Company.Text, Convert.ToInt32(buyer_id));
                            BuyerDataGrid.ItemsSource = buyer.GetData();
                            Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Company.Text = ""; ErrorText.Text = ""; TypeBox.Text = "";
                        }
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (BuyerDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                buyer.DeleteBuyer(Convert.ToInt32(buyer_id));
                BuyerDataGrid.ItemsSource = buyer.GetData();
                Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Company.Text = ""; ErrorText.Text = ""; TypeBox.Text = "";
            }
        }
    }
}
