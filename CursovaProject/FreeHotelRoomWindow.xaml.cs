using CursovaProject.Rooms;
using System.Windows;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for DeleteResidentWindow.xaml
  /// </summary>
  public partial class FreeHotelRoom : Window
  {
    Hotel _currentHotel;
    DatabaseManager _databaseManager;
    public FreeHotelRoom(Hotel currentHotel, DatabaseManager databaseManager)
    {
      InitializeComponent();
      _currentHotel = currentHotel;
      RoomsComboBox.ItemsSource = _currentHotel.HotelRooms;
      _databaseManager = databaseManager;
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
      if (RoomsComboBox.SelectedItem != null)
      {
        int roomNumber = (RoomsComboBox.SelectedItem as HotelRoom).RoomNumber;
        HotelRoom room = _currentHotel.GetHotelRoom(roomNumber);
        _currentHotel.TotalIncome += room.GetTotalPriceInTheEnd();
        _databaseManager.ChangeTotalIncome(_currentHotel);
        _currentHotel.DeleteRoom(roomNumber);
        _databaseManager.DeleteRoom(_currentHotel, roomNumber);
      }
      else
      {
        MessageBox.Show("Будь ласка виберіть номер готелю", "Помилка при виборі номеру", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}
