using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPO_RKOT_UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DataBaseViewWindow.xaml
    /// </summary>
    public partial class DataBaseViewWindow : Window
    {
        public DataBaseViewWindow(ReportInfo reportInfo)
        {
            InitializeComponent();
            ReportInfo = reportInfo;
            DataContext = ReportInfo;
            excelDataGrid.ItemsSource = ReportInfo.Reports;
        }

        public ReportInfo ReportInfo { get; set; }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void PanelControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void PanelControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ReportInfoSaveChanged();
        }

        private void ReportInfoSaveChanged()
        {
            using (var context = new RkotContext())
            {
                context.Update(ReportInfo);
                context.SaveChanges();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveChangesWindowDialog saveChangesWindowDialog = new SaveChangesWindowDialog();
            saveChangesWindowDialog.ShowDialog();
            if (saveChangesWindowDialog.DialogResult == SaveChangesWindowDialog.CustomDialogResult.Yes)
            {
                ReportInfoSaveChanged();
                MessageBox.Show("Данные сохранены");
            }
            else if (saveChangesWindowDialog.DialogResult == SaveChangesWindowDialog.CustomDialogResult.No)
            {
                MessageBox.Show("Данные не сохранены");
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
