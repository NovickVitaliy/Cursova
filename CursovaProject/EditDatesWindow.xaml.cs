using System;
using System.Windows;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for EditDatesWindow.xaml
  /// </summary>
  public partial class EditDatesWindow : Window
  {
    private Hotel _currentHotel;
    private DatabaseManager _databaseManager;
    private int _roomNumber;
    public EditDatesWindow(Hotel currentHotel, DatabaseManager databaseManager, int roomNumber)
    {
      _currentHotel = currentHotel;
      _databaseManager = databaseManager;
      _roomNumber = roomNumber;
      InitializeComponent();
      dpDateIn.SelectedDate = _currentHotel.GetHotelRoom(roomNumber).DateOfCheckIn;
      dpDateOut.SelectedDate = _currentHotel.GetHotelRoom(_roomNumber).DateOfCheckOut;
    }
    private void EditDateButton_Click(object sender, RoutedEventArgs e)
    {
      DateTime? newDateIn = dpDateIn.SelectedDate;
      DateTime? newDateOut = dpDateOut.SelectedDate;
      if(newDateIn == null || newDateOut == null ) 
      {
        MessageBox.Show("Виберіть дати.", "Дата не була вибрана", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }
      if(newDateIn.Value > newDateOut.Value )
      {
        MessageBox.Show("Дата заїзду не може бути пізніше дати виїзду", "Неправильна дата", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }
      _currentHotel.ChangeDatesOfRooms(_roomNumber, newDateIn.Value, newDateOut.Value);
      _databaseManager.ChangeDatesOfRoom(_currentHotel, _roomNumber, newDateIn.Value, newDateOut.Value);
      MessageBox.Show("Дати було успішно змінено", "Зміна дати", MessageBoxButton.OK, MessageBoxImage.Information);
    }
  }
}
