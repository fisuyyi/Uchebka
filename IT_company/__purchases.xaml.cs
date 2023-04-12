using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
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
    /// Логика взаимодействия для __purchases.xaml
    /// </summary>
    public partial class __purchases : Page
    {
        purchasesTableAdapter purchases = new purchasesTableAdapter();
        price_listTableAdapter list = new price_listTableAdapter();
        buyerTableAdapter buyer = new buyerTableAdapter();
        payment_typeTableAdapter payment = new payment_typeTableAdapter();
        staffTableAdapter staff = new staffTableAdapter();
        public __purchases()
        {
            InitializeComponent();
            PurchasesDataGrid.ItemsSource = purchases.GetData();
            PriceBox.ItemsSource = list.GetData();
            PriceBox.DisplayMemberPath = "name_service";
            BuyerBox.ItemsSource = buyer.GetData();
            BuyerBox.DisplayMemberPath = "name_";
            PaymentBox.ItemsSource = payment.GetData();
            PaymentBox.DisplayMemberPath = "payment_type";
            StaffBox.ItemsSource = staff.GetData();
            StaffBox.DisplayMemberPath = "name_staff";
        }
        public object purchases_id;
        private void PurchasesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PurchasesDataGrid.SelectedItem != null)
            {
                purchases_id = (PurchasesDataGrid.SelectedItem as DataRowView).Row[0];
                int priceId = Convert.ToInt32((PurchasesDataGrid.SelectedItem as DataRowView).Row[1]);
                var allPrices = list.GetData().Rows;
                for (int i = 0; i < allPrices.Count; i++)
                {
                    if (Convert.ToInt32(allPrices[i][0]) == priceId)
                    {
                        PriceBox.Text = allPrices[i][1].ToString();
                    }
                }

                int buyerId = Convert.ToInt32((PurchasesDataGrid.SelectedItem as DataRowView).Row[2]);
                var allBuyer = buyer.GetData().Rows;
                for (int i = 0; i < allBuyer.Count; i++)
                {
                    if (Convert.ToInt32(allBuyer[i][0]) == buyerId)
                    {
                        BuyerBox.Text = allBuyer[i][3].ToString();
                    }
                }

                int TypeId = Convert.ToInt32((PurchasesDataGrid.SelectedItem as DataRowView).Row[3]);
                var allTypes = payment.GetData().Rows;
                for (int i = 0; i < allTypes.Count; i++)
                {
                    if (Convert.ToInt32(allTypes[i][0]) == TypeId)
                    {
                        PaymentBox.Text = allTypes[i][1].ToString();
                    }
                }

                int staffID = Convert.ToInt32((PurchasesDataGrid.SelectedItem as DataRowView).Row[4]);
                var allStaff = staff.GetData().Rows;
                for (int i = 0; i < allStaff.Count; i++)
                {
                    if (Convert.ToInt32(allStaff[i][0]) == staffID)
                    {
                        StaffBox.Text = allStaff[i][2].ToString();
                    }
                }
            }
        }
        public object price_list;
        public object buyerr;
        public object typp;
        public object staffe;
        private void PriceBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PriceBox.SelectedItem != null)
            {
                price_list = (PriceBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void BuyerBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyerBox.SelectedItem != null)
            {
                buyerr = (BuyerBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void PaymentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PaymentBox.SelectedItem != null)
            {
                typp = (PaymentBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void StaffBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffBox.SelectedItem != null)
            {
                staffe = (StaffBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (BuyerBox.SelectedItem == null | PriceBox.SelectedItem == null | PaymentBox.SelectedItem == null
                | StaffBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                purchases.AddPurchases(Convert.ToInt32(price_list), Convert.ToInt32(buyerr), Convert.ToInt32(typp),
                    Convert.ToInt32(staffe));
                PurchasesDataGrid.ItemsSource = purchases.GetData();
                ErrorText.Text = "";
                BuyerBox.Text = "";
                PaymentBox.Text = "";
                PriceBox.Text = "";
                StaffBox.Text = "";
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (PurchasesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (BuyerBox.SelectedItem == null | PriceBox.SelectedItem == null | PaymentBox.SelectedItem == null
                | StaffBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else
            {
                purchases.UpdatePurchases(Convert.ToInt32(price_list), Convert.ToInt32(buyerr), Convert.ToInt32(typp),
                    Convert.ToInt32(staffe), Convert.ToInt32(purchases_id));
                PurchasesDataGrid.ItemsSource = purchases.GetData();
                ErrorText.Text = "";
                BuyerBox.Text = "";
                PaymentBox.Text = "";
                PriceBox.Text = "";
                StaffBox.Text = "";
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (PurchasesDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку.";
            }
            else
            {
                purchases.DeletePurchases(Convert.ToInt32(purchases_id));
                PurchasesDataGrid.ItemsSource = purchases.GetData();
                ErrorText.Text = "";
                BuyerBox.Text = "";
                PaymentBox.Text = "";
                PriceBox.Text = "";
                StaffBox.Text = "";
            }
        }
    }
}
