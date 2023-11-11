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
        private ObservableCollection<ReportInfo> reportsFromDB;

        public ObservableCollection<ReportInfo> ReportsFromDB
        {
            get => reportsFromDB;
            set
            {
                reportsFromDB = value;
                OnPropertyChanged(nameof(ReportsFromDB));
            }
        }
        public ICommand ShowDataBaseViewWindowCommand { get; }

        public HomeViewModel()
        {
            LoadData();
            ShowDataBaseViewWindowCommand = new ViewModelCommand(ShowDataBaseViewWindow);
        }

        public void Update()
        {
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new RkotContext())
            {
                var reportInfos = context.ReportInfos.AsNoTracking()
                    .Include(ri => ri.Reports).ThenInclude(r => r.Stat)
                    .Include(ri => ri.Reports).ThenInclude(r => r.SmsQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.VoiceQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.HttpQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.Operator);
                ReportsFromDB = new ObservableCollection<ReportInfo>(reportInfos.ToList());
            }
        }

        private void ShowDataBaseViewWindow(object obj)
        {
            // var dataBase = new DataBaseViewWindow();
            // dataBase.ShowDialog();
        }
    }
}
