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
using System.Windows.Navigation;
using System.Windows.Shapes;
using IT_company.ITDataSetTableAdapters;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        authTableAdapter login = new authTableAdapter();
        staffTableAdapter staff = new staffTableAdapter();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var allStaff = staff.GetData().Rows;
            var allLogins = login.GetData().Rows;
            for (int i = 0; i < allLogins.Count; i++)
            {
                if (allLogins[i][1].ToString() == LoginTBX.Text && allLogins[i][2].ToString() == PasswordTBX.Password)
                {
                    int staffID = Convert.ToInt32(allLogins[i][3]);
                    for (int j = 0; j < allStaff.Count; j++)
                    {
                        if (Convert.ToInt32(allStaff[j][0]) == staffID)
                        {
                            int roleID = (int)allStaff[j][6];
                            switch (roleID)
                            {
                                case 1: 
                                    Admin admin = new Admin();
                                    admin.Show();
                                    Close();
                                    break;
                                case 2:
                                    User user = new User();
                                    user.Show();
                                    Close();
                                    break;
                                case 3:
                                    Cashier cashier = new Cashier();
                                    cashier.Show(); 
                                    Close();
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Default.Text = "Вы неправильно ввели логин или пароль. Попробуйте заново!";
                }
            }

        }
    }
}
