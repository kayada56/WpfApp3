﻿using System;
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

namespace WpfApp3
{
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
            DataContext = this; // Устанавливаем текущий объект как источник данных
        }

        public string CurrentUsername => LoginForm.CurrentUsername;

        private void store_button_Click(object sender, RoutedEventArgs e)
        {
            WeatherWindow weatherWindow = new WeatherWindow();
            weatherWindow.Show();
            this.Close();
        }
    }
}