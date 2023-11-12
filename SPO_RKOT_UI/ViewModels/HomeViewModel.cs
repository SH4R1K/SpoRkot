using Microsoft.EntityFrameworkCore;
using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SPO_RKOT_UI.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private ObservableCollection<ReportInfo> reportsFromDB;
        private string textMessage;

        public ObservableCollection<ReportInfo> ReportsFromDB
        {
            get => reportsFromDB;
            set
            {
                reportsFromDB = value;
                OnPropertyChanged(nameof(ReportsFromDB));
            }
        }

        public string TextMessage
        {
            get => textMessage;
            set
            {
                textMessage = value;
                OnPropertyChanged(nameof(TextMessage));
            }
        }

        public  HomeViewModel()
        {
            try
            {
                LoadDataAsync();
            }
            catch (Exception ex)
            {
                reportsFromDB = null;
                TextMessage = ex.Message;
            }
        }

        public async Task Update()
        {
            try
            {
                await LoadDataAsync();
            }
             catch (Exception ex)
            {
                reportsFromDB = null;
                TextMessage = ex.Message;
            }
        }

        private async Task LoadDataAsync()
        {
            using (var context = new RkotContext())
            {
                try
                {
                    var reportInfos = context.ReportInfos.AsNoTracking()
                    .Include(ri => ri.Reports).ThenInclude(r => r.Stat)
                    .Include(ri => ri.Reports).ThenInclude(r => r.SmsQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.VoiceQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.HttpQuality)
                    .Include(ri => ri.Reports).ThenInclude(r => r.Operator);
                    ReportsFromDB = new ObservableCollection<ReportInfo>(await reportInfos.ToListAsync());
                    TextMessage = string.Empty;
                }
                catch (Exception)
                {
                    throw new Exception("Проблема с загрузкой данных. Проверьте подключение к интернету.");
                }
            }

        }
    }
}
