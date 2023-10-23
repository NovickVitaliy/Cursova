using System.Collections.ObjectModel;
using System.Windows;
namespace CursovaProject
{
    /// <summary>
    /// Interaction logic for DeleteHotelButton.xaml
    /// </summary>
    public partial class DeleteHotelButton : Window
    {
        ObservableCollection<Hotel> hotelList;
        DatabaseManager _databaseManager;
        public DeleteHotelButton(ObservableCollection<Hotel> hotels, DatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
            InitializeComponent();
            hotelList = hotels;
            HotelsComboBox.ItemsSource = hotels;
        }
        private void OnDeleteHotelButton_Click(object sender, RoutedEventArgs e)
        {
            if(HotelsComboBox.SelectedItem != null)
            {
                _databaseManager.DeleteDatabase(HotelsComboBox.SelectedItem.ToString());
                hotelList.Remove(HotelsComboBox.SelectedItem as Hotel);
            }
            else
            {
                MessageBox.Show("Будь ласка виберіть готель, базу даних якого потрібно видалити", "Готель не було вибрано", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
