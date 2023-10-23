using CursovaProject.Rooms;
using System.Windows;
namespace CursovaProject
{
    /// <summary>
    /// Interaction logic for FindInfoByRoomNumber.xaml
    /// </summary>
    public partial class FindInfoByRoomNumber : Window
    {
        Hotel _currentHotel;
        public FindInfoByRoomNumber(Hotel currentHotel)
        {
            InitializeComponent();
            _currentHotel = currentHotel;
            RoomsComboBox.ItemsSource = _currentHotel.HotelRooms;
        }
        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(RoomsComboBox.SelectedItem != null)
            {
                HotelRoom room = RoomsComboBox.SelectedItem as HotelRoom;

                MessageBox.Show(room.GetInfo());
            }
            else
            {
                MessageBox.Show("Номер готелю не було вибрано", "Помилка при виборі номеру", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
