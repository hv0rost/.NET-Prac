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

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для NewGroup.xaml
    /// </summary>
    public partial class NewGroup : Window
    {
        public NewGroup()
        {
            InitializeComponent();
        }

        private void OkButton_Click1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
