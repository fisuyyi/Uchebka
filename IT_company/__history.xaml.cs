using IT_company.ITDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для __history.xaml
    /// </summary>
    public partial class __history : Page
    {
        history_purchasesTableAdapter history = new history_purchasesTableAdapter();
        purchasesTableAdapter purchases = new purchasesTableAdapter();
        public __history()
        {
            InitializeComponent();
            HistoryDataGrid.ItemsSource = history.GetData();
            PurchasesBox.ItemsSource = purchases.GetData();
            PurchasesBox.DisplayMemberPath = "id_purchases";
        }
        public object history_id;
        private void HistoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HistoryDataGrid.SelectedItem != null)
            {
                history_id = (HistoryDataGrid.SelectedItem as DataRowView).Row[0];
                int priceId = Convert.ToInt32((HistoryDataGrid.SelectedItem as DataRowView).Row[1]);
                var allPurchases = purchases.GetData().Rows;
                for (int i = 0; i < allPurchases.Count; i++)
                {
                    if (Convert.ToInt32(allPurchases[i][0]) == priceId)
                    {
                        PurchasesBox.Text = allPurchases[i][0].ToString();
                    }
                }
                string dates = Convert.ToString((HistoryDataGrid.SelectedItem as DataRowView).Row[2]);
                DatePick.Text = dates;
                DateTime date = Convert.ToDateTime(DatePick.Text);
            }
            

        }
        public object purchases_id;
        private void PurchasesBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PurchasesBox.SelectedItem != null)
            {
                purchases_id = (PurchasesBox.SelectedItem as DataRowView).Row[0];
            }

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DatePick.SelectedDate == null | PurchasesBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены.";
            }
            else
            {
                history.AddHistory(Convert.ToInt32(purchases_id), DatePick.Text);
                HistoryDataGrid.ItemsSource = history.GetData();
                ErrorText.Text = ""; PurchasesBox.SelectedItem = null;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите изменяемую строку";
            }
            else if (DatePick.SelectedDate == null | PurchasesBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены.";
            }
            else
            {
                history.UpdateHistory(Convert.ToInt32(purchases_id), DatePick.Text, Convert.ToInt32(history_id));
                HistoryDataGrid.ItemsSource = history.GetData();
                ErrorText.Text = ""; PurchasesBox.SelectedItem = null;
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку";
            }
            else
            {
                history.DeleteHistory(Convert.ToInt32(history_id));
                HistoryDataGrid.ItemsSource = history.GetData();
                ErrorText.Text = ""; PurchasesBox.SelectedItem = null;
            }
        }
    }
}
