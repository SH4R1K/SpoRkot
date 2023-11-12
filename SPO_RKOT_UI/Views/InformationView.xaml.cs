using SpoRkotLibrary.Models;
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
    /// Логика взаимодействия для InformationView.xaml
    /// </summary>
    public partial class InformationView : UserControl
    {
        public InformationView()
        {
            InitializeComponent();
        }

        //Кнопка Подробнее
        private void MoreInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var infoView = new MoreInfoView();
            infoView.Show();
        }
    }
}
