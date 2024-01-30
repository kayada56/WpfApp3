using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp3
{
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password)
            {
                using (var sha256 = new SHA256Cng())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }

        public static class LoginHasher
        {
            public static string HashLogin(string login)
            {
                using (var sha256 = new SHA256Cng())
                {
                    byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(login));
                    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userNameField.Text == "")
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (userPassField.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (isUserExists())
            {
                return;
            }

            string hashedPassword = PasswordHasher.HashPassword(userPassField.Text);
            string hashedLogin = LoginHasher.HashLogin(userNameField.Text);

            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`login`, `pass`) VALUES (@login, @pass)", db.GetConnection());

            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = hashedLogin;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = hashedPassword;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан");
            else
                MessageBox.Show("Аккаунт не был создан");

            db.closeConnection();
        }

        public bool isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login`= @uL ", db.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userNameField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже существует");
                return true;
            }
            else
                return false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginForm logForm = new LoginForm();
            logForm.Show();
            this.Close();
        }
    }
}