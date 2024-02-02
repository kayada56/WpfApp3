using System;
using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using System.Data;
using System.Runtime;
using static WpfApp3.RegisterForm;
using System.Security.Cryptography;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public static class HasherLogin
        {
            public static string HashString(string login)
            {
                using (var sha256 = new SHA256Cng())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(login));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }

        public static class HasherPassword
        {
            public static string HashString(string password)
            {
                using (var sha256 = new SHA256Cng())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }

        public static string CurrentUsername { get; private set; }


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            String loginUser = loginField.Text;
            String passUser = passField.Text;

            string hashedLogin = HasherLogin.HashString(loginUser);
            string hashedPassword = HasherPassword.HashString(passUser);

            DB db = new DB();

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`= @uL AND `pass`= @uP", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = hashedLogin;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = hashedPassword;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                CurrentUsername = loginUser;
                MessageBox.Show("Успешный вход");

                Profile profWindow = new Profile();
                profWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }
    }
}
