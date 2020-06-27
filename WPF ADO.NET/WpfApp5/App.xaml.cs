using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Model.EFContext db = new Model.EFContext();
        protected override void OnLoadCompleted(NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            db.SaveChanges();
            base.OnExit(e);
        }
    }
}
