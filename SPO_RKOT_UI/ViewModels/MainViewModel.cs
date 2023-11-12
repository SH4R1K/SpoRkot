using FontAwesome.Sharp;
using System.Windows.Input;

namespace SPO_RKOT_UI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;

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

        //--> Commands (viewmodels)
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowInformationViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        public MainViewModel()
        {
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowInformationViewCommand = new ViewModelCommand(ExecuteShowInformationViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteShowSettingsViewCommand);
            //Default view
            ExecuteShowHomeViewCommand(true);
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Список Отчётов";
            Icon = IconChar.List;
        }

        private void ExecuteShowInformationViewCommand(object obj)
        {
            CurrentChildView = new InformationViewModel();
            Caption = "Справка";
            Icon = IconChar.Question;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = new SettingsViewModel();
            Caption = "Настройки";
            Icon = IconChar.Tools;
        }
    }
}
