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
    /// Логика взаимодействия для MoreInfoView.xaml
    /// </summary>
    public partial class MoreInfoView : Window
    {
        public MoreInfoView()
        {
            InitializeComponent();
        }
        
        //Кнопка Закрыть(крестик)
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
