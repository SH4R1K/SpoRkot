using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace SPO_RKOT_UI.Views
{
    /// <summary>
    /// Логика взаимодействия для DataBaseViewWindow.xaml
    /// </summary>
    public partial class DataBaseViewWindow : Window
    {
        bool isCancel;

        public DataBaseViewWindow(ReportInfo reportInfo)
        {
            InitializeComponent();

            isCancel = false;
            ReportInfo = reportInfo;
            DataContext = ReportInfo;
            excelDataGrid.ItemsSource = ReportInfo.Reports;
            List<DataGridColumn> dataGridColumns = new List<DataGridColumn>();
            foreach (DataGridColumn dataGridColumn in excelDataGrid.Columns)
            {
                dataGridColumns.Add(dataGridColumn);
            }
            excelDataGrid.Columns.Clear();
            dataGridColumns.Reverse();
            foreach (DataGridColumn dataGridColumn in dataGridColumns)
            {
                excelDataGrid.Columns.Add(dataGridColumn);
            }
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
            var button = sender as Button;
            if (button.Tag.ToString() == "Cancel")
                isCancel = true;
            Close();
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
            try
            {
                using (var context = new RkotContext())
                {
                    context.Update(ReportInfo);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Проблема с сохранением. Данные не сохранены. Проверьте подключение к Интернету или обратитесь к системному администратору.");
            }

        }
        private void DgridScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Add MouseWheel support for the datagrid scrollviewer.
            excelDataGrid.AddHandler(MouseWheelEvent, new RoutedEventHandler(DataGridMouseWheelHorizontal), true);
        }
        private void DataGridMouseWheelHorizontal(object sender, RoutedEventArgs e)
        {
            MouseWheelEventArgs eargs = (MouseWheelEventArgs)e;
            double x = (double)eargs.Delta;
            double y = dgridScrollViewer.VerticalOffset;
            dgridScrollViewer.ScrollToVerticalOffset(y - x);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isCancel)
            {
                SaveChangesWindowDialog saveChangesWindowDialog = new SaveChangesWindowDialog();
                saveChangesWindowDialog.ShowDialog();
                if (saveChangesWindowDialog.DialogResult == SaveChangesWindowDialog.CustomDialogResult.Yes)
                {
                    ReportInfoSaveChanged();
                    MessageBox.Show("Данные сохранены");
                }
                else if (saveChangesWindowDialog.DialogResult == SaveChangesWindowDialog.CustomDialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

