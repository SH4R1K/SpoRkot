using FontAwesome.Sharp;
using System.Windows.Input;

namespace SPO_RKOT_UI.ViewModels
{
    /// <summary>
    /// ViewModel для MainView
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;

        /// <summary>
        /// Ссылка на текущую страницу
        /// </summary>
        public ViewModelBase CurrentChildView
        {
            get
            {
                return _currentChildView;
            }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));
            }
        }

        private string _caption;

        /// <summary>
        /// Название страницы
        /// </summary>
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }

        private IconChar _icon;

        /// <summary>
        /// Иконка страницы
        /// </summary>
        public IconChar Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        //--> Команды (ViewModels)
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowInformationViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        /// <summary>
        /// Конструктор для объекта ViewModel для MainView
        /// </summary>
        public MainViewModel()
        {
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowInformationViewCommand = new ViewModelCommand(ExecuteShowInformationViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);

            //View по умолчанию
            ExecuteShowHomeViewCommand(true);
        }

        /// <summary>
        /// Открывает страницу для отображения списка отчёта
        /// </summary>
        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Список Отчётов";
            Icon = IconChar.List;
        }

        /// <summary>
        /// Открывает страницу для отображения справки
        /// </summary>
        private void ExecuteShowInformationViewCommand(object obj)
        {
            CurrentChildView = new ViewModelBase();
            Caption = "Справка";
            Icon = IconChar.Question;
        }

        /// <summary>
        /// Открывает страницу для настроек
        /// </summary>
        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Настройки";
            Icon = IconChar.Tools;
        }
    }
}
