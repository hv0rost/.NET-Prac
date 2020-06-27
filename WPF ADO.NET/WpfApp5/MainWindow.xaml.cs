using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model.EFContext db = (Application.Current as App).db;

        CollectionViewSource studentViewSource;
        CollectionViewSource groupViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Groups.Load();
            db.Students.Load();
            //output.Text = "";
            //foreach (var student in db.Students)
            //{
            //    output.Text += $"{student.Name} - " +
            //        $"{student.Group.Track}-{student.Group.Course}{student.Group.Number}\n";
            //}


            studentViewSource = ((CollectionViewSource)(this.FindResource("studentViewSource")));
            studentViewSource.Source = db.Students.Local;

            groupViewSource = (CollectionViewSource)this.FindResource("groupViewSource");
            groupViewSource.Source = db.Groups.Local;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (db.HasChanges)
            {
                int changes = 0;
                try
                {
                    changes = db.SaveChanges();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    MessageBox.Show($"{changes} изменений сохранено");
                }
            }
        }

        private void ContentControl_LostFocus(object sender, RoutedEventArgs e)
        {
            studentViewSource.View.Refresh();
            groupViewSource.View.Refresh();
        }

        private void AddStudent_Click(object sender, EventArgs e)
        {
            //var student = new Model.Student { Name = "Test", Admission = DateTime.Now, GroupId = 0 };

            var newStudentDialog = new NewStudent();
            if(newStudentDialog.ShowDialog() == true)
            {
                var student = (Model.Student)(newStudentDialog.FindResource("studentViewSource"));
                db.Students.Add(student);
            }           
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            var removedStudent = studentsList.SelectedItem as Model.Student;
            db.Students.Remove(removedStudent);
        }


        private void AddGroup_Click(object sender, EventArgs e)
        {

            var newGroupDialog = new NewGroup();
            if (newGroupDialog.ShowDialog() == true)
            {
                var group = (Model.Group)(newGroupDialog.FindResource("groupViewSource"));
                db.Groups.Add(group);
            }
        }

        private void RemoveGroup_Click(object sender, EventArgs e)
        {
            var removedGroup = groupsList.SelectedItem as Model.Group;
            db.Groups.Remove(removedGroup);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var export = new StudentsToXLSProvider(studentViewSource.Source as IEnumerable<Model.Student>);

            export.ExportTo("test.xlsx");
        }
    }
}
