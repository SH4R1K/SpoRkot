using ExcelLibrary;
using Microsoft.Win32;
using SPO_RKOT_UI.ViewModels;
using SpoRkotLibrary.Data;
using SpoRkotLibrary.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace SPO_RKOT_UI.Views
{
    /// <summary>
    /// Логика взаимодействия для HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel homeViewModel = new HomeViewModel();

        public HomeView()
        {
            InitializeComponent();

            DataContext = homeViewModel;
        }

        //Добавление данных из excel-файла
        private async void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fileName;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    fileName = openFileDialog.FileName;
                    if (await ExcelReader.ImportFromExcelAsync(fileName))
                        MessageBox.Show("Отчет успешно добавлен.");
                    else
                        MessageBox.Show("Отчет с такими данными уже есть");
                    await homeViewModel.UpdateAsync();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Во время загрузки excel-файла возникли ошибки. Попробуйте загрузить его заново.","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Открытие отчета при двойном щелчке
        private async void UserList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await OpenTableViewAsync();
        }

        //Кнопка Открыть
        private void WatchButtonInRow_Click(object sender, RoutedEventArgs e)
        {
            ReportInfo report = (sender as System.Windows.Controls.Button)?.DataContext as ReportInfo;
            var dataBase = new DataBaseViewWindow(report);
            dataBase.ShowDialog();
            homeViewModel.UpdateAsync();
        }

        /// <summary>
        /// Открытие отчета
        /// </summary>
        private async Task OpenTableViewAsync()
        {
            if (reportsListView.SelectedItem == null) return;
            ReportInfo reportInfo = (ReportInfo)reportsListView.SelectedItem;
            var dataBase = new DataBaseViewWindow(reportInfo);
            dataBase.ShowDialog();
            await homeViewModel.UpdateAsync();
        }

        //Кнопка Обновить
        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            await homeViewModel.UpdateAsync();
        }

        //Горячие клавиши
        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                UpdateButton_Click(sender, e);
            }
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.N)
            {
                SelectFileButton_Click(sender, e);
            }
        }

        //Фильтрация
        private void FindLocationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            reportsListView.Items.Filter = FilterListView; //поиск по listview
        }

        private void FindDistrictTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            reportsListView.Items.Filter = FilterListView; //поиск по listview
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            reportsListView.Items.Filter = FilterListView; //поиск по listview
        }

        private bool FilterListView(object obj)
        {
            var report = (ReportInfo)obj;

            bool startDateFilter = !startDatePicker.SelectedDate.HasValue || report.StartDate >= startDatePicker.SelectedDate.Value;
            bool endDateFilter = !endDatePicker.SelectedDate.HasValue || report.EndDate <= endDatePicker.SelectedDate.Value;
            bool districtFilter = string.IsNullOrEmpty(findDistrictTextBox.Text) || report.FederalDistrict.Contains(findDistrictTextBox.Text, StringComparison.OrdinalIgnoreCase);
            bool locationFilter = string.IsNullOrEmpty(findLocationTextBox.Text) || report.Location.Contains(findLocationTextBox.Text, StringComparison.OrdinalIgnoreCase);

            return startDateFilter && endDateFilter && districtFilter && locationFilter;
        }

        private void ClearFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            findLocationTextBox.Text = string.Empty;
            findDistrictTextBox.Text = string.Empty;
            endDatePicker.SelectedDate = null;
            startDatePicker.SelectedDate = null;
            reportsListView.Items.Filter = FilterListView;
        }


        //Сортировка
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowUp"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                          Resources["HeaderTemplateArrowDown"] as DataTemplate;
                    }

                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }

                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }

            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            try
            {
                ICollectionView dataView =
                CollectionViewSource.GetDefaultView(reportsListView.ItemsSource);

                dataView.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                dataView.SortDescriptions.Add(sd);
                dataView.Refresh();
            }
            catch (NullReferenceException)
            {
                MessageBox.Show($"Данные отсутствуют", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Неизвестная Ошибка: \n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Кнопка Удалить
        private async void DeleteReportButton_Click(object sender, RoutedEventArgs e)
        {
            var answer = MessageBox.Show("Вы уверены, что хотите удалить отчёт?", "Удаление отчёта",MessageBoxButton.YesNo,MessageBoxImage.Warning);
            if (answer == MessageBoxResult.Yes)
            {
                try
                {
                    using (var context = new RkotContext())
                    {
                        ReportInfo report = (sender as Button)?.DataContext as ReportInfo;
                        context.ReportInfos.Remove(report);
                        context.SaveChanges();
                        await homeViewModel.UpdateAsync();
                    }
                }
                catch(Exception)
                {
                    MessageBox.Show("Данные не были удалены, либо были удалены ранее");
                    await homeViewModel.UpdateAsync();
                }
            }
        }
    }
}
