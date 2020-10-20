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
using FurnitureOrder.dataBase;
using System.Text.RegularExpressions;

namespace FurnitureOrder.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        MainWindow main;
        public Registration(MainWindow main)
        {
            this.main = main;
            InitializeComponent();
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (CheckPassword())
            {
                User user = new User();
                user.Имя = name.Text;
                user.Фамилия = surname.Text;
                user.Отчество = patronymic.Text;
                user.login = login.Text;
                user.password = password.Password;
                user.role = "заказчик";
                try
                {
                    main.bd.User.Add(user);
                    main.bd.SaveChanges();
                    RegistrationFrame.Navigate(new Login(main));
                    MessageBox.Show("you are registered");
                }
                catch
                {
                    main.bd.User.Remove(user);
                    MessageBox.Show("Error! user already exists");
                }
            }
        }
        private bool CheckPassword()
        {
            if (password.Password.Length >= 6 && password.Password.Length <= 18 &&
                !Regex.IsMatch(password.Password, @"(.)\1{2}") &&
                Regex.IsMatch(password.Password, @"[1-9]{1}") &&
                Regex.IsMatch(password.Password, @"[*&{}|+.]{1}"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Password rulesl" + "\n" +
                    "1) password should include symbol: *&{}|+." + "\n" +
                    "2) password should include numberl" + "\n" +
                    "3) Three identical symbols in a row cannot be repeated" + "\n" +
                    "4) Password must be between 6 and 18 characters");
                return false;
            }
        }
    }
}
