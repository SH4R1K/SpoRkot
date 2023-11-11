using Microsoft.Office.Interop.Excel;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using SPO_RKOT_UI.ClassWork;
using SPO_RKOT_UI.ViewModels;
using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            var homeViewModel = new HomeViewModel();
            DataContext = homeViewModel;


        }

        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO поиск, справка, тема
            var fileName = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm;|All Files|*.*";  
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                ExcelReader.ImportFromExcel(fileName);
            }
                MessageBox.Show("Работает без прикола!");
        }

        private void FindTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            reportsListView.Items.Filter = FilterMethod; //поиск по listview
        }

        private bool FilterMethod(object obj)
        {
            var user = (ReportInfo)obj;
            return user.Location.Contains(findTextBox.Text, StringComparison.OrdinalIgnoreCase);
        }

        private void UserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (reportsListView.SelectedItem == null) return;
            ReportInfo reportInfo = (ReportInfo)reportsListView.SelectedItem;
            var dataBase = new DataBaseViewWindow(reportInfo);
            dataBase.ShowDialog();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("обновление заглушка");
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                UpdateButton_Click(sender, e);
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.N)
            {
                SelectFileButton_Click(sender, e);
            }
        }
    }
}
