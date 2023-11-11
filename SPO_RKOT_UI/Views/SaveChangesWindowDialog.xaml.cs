using System.Windows;

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
            Close,
            Yes,
            No,
            Cancel
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.Yes;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.No;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = CustomDialogResult.Cancel;
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult == CustomDialogResult.Close)
                DialogResult = CustomDialogResult.Cancel;
        }
    }
}
