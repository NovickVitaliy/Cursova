using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for RegisterResidentWindow.xaml
  /// </summary>
  public partial class RegisterResidentWindow : Window
  {
    private Hotel _hotel;
    private ItemsControl _itemsControl;
    private DatabaseManager _databaseManager;
    public RegisterResidentWindow(Hotel hotel, ItemsControl itemsControl, DatabaseManager databaseManager)
    {
      _hotel = hotel;
      _itemsControl = itemsControl;
      InitializeComponent();
      RoomsComboBox.ItemsSource = hotel.RoomTypes;
      _databaseManager = databaseManager;
    }
    private void RegisterRoomButton_OnClick_Click(object sender, RoutedEventArgs e)
    {
      if (RoomsComboBox.SelectedItem != null)
      {
        string chosenValue = RoomsComboBox.SelectedItem.ToString();
        RoomTypes roomType = GetRoomType(chosenValue);
        DateTime? dateTimeOfIn = dpDateIn.SelectedDate;
        DateTime? dateTimeOfOut = dpDateOut.SelectedDate;
        if(dateTimeOfIn == null || dateTimeOfOut == null)
        {
          MessageBox.Show("Дата не була введена", "Неправильна дата", MessageBoxButton.OK, MessageBoxImage.Warning);
          return;
        }
        if(dateTimeOfIn.Value > dateTimeOfOut.Value)
        {
          MessageBox.Show("Дата заїзду не може бути пізніше дати виїзду", "Неправильна дата", MessageBoxButton.OK, MessageBoxImage.Warning);
          return;
        }
        if (_hotel.GetAvailableRoomsBasedOnType(roomType) > 0)
        {
          _hotel.RegisterRoom(roomType, dateTimeOfIn.Value, dateTimeOfOut.Value);
          var room = _hotel.HotelRooms.Last();
          _databaseManager.SaveHotelRoom(room.RoomNumber, room.RoomType, _hotel, dateTimeOfIn.Value, dateTimeOfOut.Value);
        }
        else
        {
          MessageBox.Show($"Наразі, усі номери типу - {roomType.ToString()} заняті.", "Недостатньо номерів", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      else
      {
        MessageBox.Show("Будь ласка, виберіть тип номеру", "Помилка при виборі типу номера", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
    private RoomTypes GetRoomType(string value)
    {
      switch (value)
      {
        case "Standart Room": return RoomTypes.StandartRoom;
        case "Superior Room": return RoomTypes.SuperiorRoom;
        case "President Room": return RoomTypes.PresidentRoom;
        default: throw new Exception("Сталася непередбачувана помилка");
      }
    }
  }
}
