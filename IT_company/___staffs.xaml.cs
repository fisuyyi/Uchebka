using System;
using System.Collections.Generic;
using System.Data;
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
using IT_company.ITDataSetTableAdapters;

namespace IT_company
{
    /// <summary>
    /// Логика взаимодействия для ___staffs.xaml
    /// </summary>
    public partial class ___staffs : Page
    {
        staffTableAdapter staffs = new staffTableAdapter();
        departmentTableAdapter de = new departmentTableAdapter();
        role_sTableAdapter roles = new role_sTableAdapter();
        public ___staffs()
        {
            InitializeComponent();
            StaffDataGrid.ItemsSource = staffs.GetData();
            DepartmentBox.ItemsSource = de.GetData();
            DepartmentBox.DisplayMemberPath = "department_name";
            RoleBox.ItemsSource = roles.GetData();
            RoleBox.DisplayMemberPath = "role_name";
        }
        public object staff_id;
        private void RolesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffDataGrid.SelectedItem != null)
            {
                staff_id = (StaffDataGrid.SelectedItem as DataRowView).Row[0];
                Surname.Text = (string)(StaffDataGrid.SelectedItem as DataRowView).Row[1];
                Name.Text = (string)(StaffDataGrid.SelectedItem as DataRowView).Row[2];
                Middle.Text = (string)(StaffDataGrid.SelectedItem as DataRowView).Row[3];
                Salary.Text = Convert.ToString((StaffDataGrid.SelectedItem as DataRowView).Row[4]);
                int departmentId = Convert.ToInt32((StaffDataGrid.SelectedItem as DataRowView).Row[5]);
                var allDepartment = de.GetData().Rows;
                for (int i = 0; i < allDepartment.Count; i++)
                {
                    if (Convert.ToInt32(allDepartment[i][0]) == departmentId)
                    {
                        DepartmentBox.Text = allDepartment[i][1].ToString();
                    }
                }

                int roleID = Convert.ToInt32((StaffDataGrid.SelectedItem as DataRowView).Row[6]);
                var allRoles = roles.GetData().Rows;
                for (int i = 0; i < allRoles.Count; i++)
                {
                    if (Convert.ToInt32(allRoles[i][0]) == roleID)
                    {
                        RoleBox.Text = allRoles[i][1].ToString();
                    }
                }
            }
        }
        public object depar;
        public object roll;
        private void DepartmentBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DepartmentBox.SelectedItem != null)
            {
                depar = (DepartmentBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void RoleBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoleBox.SelectedItem != null)
            {
                roll = (RoleBox.SelectedItem as DataRowView).Row[0];
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var stringNumber = Salary.Text;
            bool isNumber = int.TryParse(stringNumber, out int numericValue);
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
            else if (DepartmentBox.SelectedItem == null | Surname.Text == "" | Name.Text == ""
                | Middle.Text == "" | Salary.Text == "" | RoleBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else if (isNumber != true)
            {
                ErrorText.Text = "В строке с зарплатой только целочисленные значения!";
            }
            else if (isNumber == true && numericValue <= 0)
            {
                ErrorText.Text = "Зарплата не может быть отрицательной и 0!";
            }
           
            else
            {
                staffs.AddStaff(Surname.Text, Name.Text, Middle.Text, numericValue, Convert.ToInt32(depar), Convert.ToInt32(roll));
                StaffDataGrid.ItemsSource = staffs.GetData();
                Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Salary.Text = ""; DepartmentBox.Text = ""; RoleBox.Text = ""; ErrorText.Text = ""; ;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var stringNumber = Salary.Text;
            bool isNumber = int.TryParse(stringNumber, out int numericValue);
            string pattern = @"^[а-яА-Я\s]*$";
            if (StaffDataGrid.SelectedItem == null)
            { 
                ErrorText.Text = "Выберите изменяемую строку.";
            }
            else if (Regex.IsMatch(Surname.Text, pattern, RegexOptions.IgnoreCase) != true)
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
            else if (DepartmentBox.SelectedItem == null | Surname.Text == "" | Name.Text == ""
                | Middle.Text == "" | Salary.Text == "" | RoleBox.SelectedItem == null)
            {
                ErrorText.Text = "Не все поля заполнены!";
            }
            else if (isNumber != true)
            {
                ErrorText.Text = "В строке с зарплатой только целочисленные значения!";
            }
            else if (isNumber == true && numericValue < 0)
            {
                ErrorText.Text = "Зарплата не может быть отрицательной!";
            }
            else
            {
                staffs.UpdateStaff(Surname.Text, Name.Text, Middle.Text, numericValue, Convert.ToInt32(depar), Convert.ToInt32(roll), Convert.ToInt32(staff_id));
                StaffDataGrid.ItemsSource = staffs.GetData();
                Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Salary.Text = ""; DepartmentBox.Text = ""; RoleBox.Text = ""; ErrorText.Text = "";
               
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (StaffDataGrid.SelectedItem == null)
            {
                ErrorText.Text = "Выберите удаляемую строку строку.";
            }
            else
            {
                staffs.DeleteStaff(Convert.ToInt32(staff_id));
                StaffDataGrid.ItemsSource = staffs.GetData();
                Surname.Text = ""; Name.Text = ""; Middle.Text = ""; Salary.Text = ""; DepartmentBox.Text = ""; RoleBox.Text = ""; ErrorText.Text = "";
            }
        }
    }
}
