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
    /// ViewModel для HomeView.
    /// </summary>
    public class HomeViewModel : ViewModelBase
    {

        private ObservableCollection<ReportInfo> reportsFromDB;
        
        /// <summary>
        /// Текущие данные отчеты.
        /// </summary>
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

        /// <summary>
        /// Текст сообщения об ошибки.
        /// </summary>
        public string TextMessage
        {
            get => textMessage;
            set
            {
                textMessage = value;
                OnPropertyChanged(nameof(TextMessage));
            }
        }

        /// <summary>
        /// Конструктор для объекта ViewModel для HomeView.
        /// </summary>
        public HomeViewModel()
        {
            try
            {
                LoadDataAsync();
            }
            catch (Exception ex)
            {
                ReportsFromDB = null;
                TextMessage = ex.Message;
            }
        }

        /// <summary>
        /// Обновляет данные в RepotsFromDB. При появлении ошибки изменяет текст сообщения. 
        /// </summary>
        public async Task UpdateAsync()
        {
            try
            {
                await LoadDataAsync();
            }
             catch (Exception ex)
            {
                ReportsFromDB = null;
                TextMessage = ex.Message;
            }
        }

        /// <summary>
        /// Загружаует данные в RepotsFromDB.
        /// </summary>
        /// <exception cref="Exception">Ошибка работы с базой данных</exception>
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
