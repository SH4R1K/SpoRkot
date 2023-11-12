using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace SPO_RKOT_UI.Views
{
    /// <summary>
    /// Логика взаимодействия для SaveChangesWindowDialog.xaml
    /// </summary>
    public partial class SaveChangesWindowDialog : Window
    {
        public CustomDialogResult DialogResult { get; set; }

        public SaveChangesWindowDialog()
        {
            InitializeComponent();
        }

        public enum CustomDialogResult
        {
            Cancel,
            Yes,
            No
        }

        //Кнопка Да
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.Yes;
            Close();
        }

        //Кнопка Нет
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.No;
            Close();
        }

        //Кнопка Отмена
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.Cancel;
            Close();
        }

        //Кнопка Закрыть(крестик). Срабатывает как кнопка Отмена
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult = CustomDialogResult.Cancel;
        }

        //Window Controls
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
    }
}
