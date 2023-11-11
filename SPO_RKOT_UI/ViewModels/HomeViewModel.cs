using Microsoft.EntityFrameworkCore;
using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SPO_RKOT_UI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<ReportInfo> ReportsFromDB { get; set; }

        public ICommand ShowDataBaseViewWindowCommand { get; }
        public HomeViewModel()
        {
            using (var context = new RkotContext())
            {
                var reportInfos = context.ReportInfos.Include(ri => ri.Reports).ThenInclude(r => r.Stat)
                    .Include(ri => ri.Reports).ThenInclude(r => r.SmsQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.VoiceQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.HttpQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.Operator);
                ReportsFromDB = new ObservableCollection<ReportInfo>(reportInfos.ToList());// получаем список репортов из класса для работы с бд
            }
            ShowDataBaseViewWindowCommand = new ViewModelCommand(ShowDataBaseViewWindow);
        }

        private void ShowDataBaseViewWindow(object obj)
        {
            // var dataBase = new DataBaseViewWindow();
            // dataBase.ShowDialog();
        }
    }
}
