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
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace WpfApp3
{
  
    /// <summary>
    /// Логика взаимодействия для NewStudent.xaml
    /// </summary>
    public partial class NewStudent : Window
    {
        bool closeButton = true;
        public Student Student
        {
            get
            {
                return this.Resources["NewStudent"] as Student;
            }
        }

        public NewStudent()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            closeButton = false;
            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            closeButton = false;
            this.DialogResult = false;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (closeButton)
            {
                if (MessageBox.Show("Сохранить изменения", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            }
            else { }
        }
    }
}
