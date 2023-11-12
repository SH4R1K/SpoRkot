using SpoRkotLibrary.Data;
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

namespace SPO_RKOT_UI.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }

        //Кнопка Применить
        private void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            ConnectionManager.Login = Properties.Settings.Default.login;
            ConnectionManager.DataBase = Properties.Settings.Default.database;
            ConnectionManager.Password = Properties.Settings.Default.password;
            ConnectionManager.Server = Properties.Settings.Default.server;
        }
    }
}
