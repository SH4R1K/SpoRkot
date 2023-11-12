using Microsoft.EntityFrameworkCore;
using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SPO_RKOT_UI.ViewModels
{
    /// <summary>
    /// ViewModel для HomeView
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {

        private ObservableCollection<ReportInfo> reportsFromDB;
        
        // Текущие отчеты
        public ObservableCollection<ReportInfo> ReportsFromDB
        {
            get => reportsFromDB;
            set
            {
                reportsFromDB = value;
                OnPropertyChanged(nameof(ReportsFromDB));
            }
        }

        private string textMessage;

        // Текст сообщения об ошибке
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
            Properties.Settings.Default.login = "oleg";
            Properties.Settings.Default.Save();

            ConnectionManager.Login = Properties.Settings.Default.login;
            ConnectionManager.DataBase = Properties.Settings.Default.database;
            ConnectionManager.Password = Properties.Settings.Default.password;
            ConnectionManager.Server = Properties.Settings.Default.server;
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
