using System;
using System.Windows;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for RemoveConcreteResident.xaml
  /// </summary>
  public partial class RemoveConcreteResident : Window
  {
    private Hotel _currentHotel;
    private int _roomNumber;
    private DatabaseManager _databaseManager;
    private MainWindow _mainWindow;
    public RemoveConcreteResident(Hotel currentHotel, int roomNumber, DatabaseManager databaseManager, MainWindow mainWindow)
    {
      InitializeComponent();
      _currentHotel = currentHotel;
      _roomNumber = roomNumber;
      _databaseManager = databaseManager;
      _mainWindow = mainWindow;
      ResidentsComboBox.ItemsSource = _currentHotel.GetHotelRoom(roomNumber).Residents;
    }
    private void DeleteConcreteResident_Click(object sender, RoutedEventArgs e)
    {
      if (!(ResidentsComboBox.SelectedItem is Person person))
      {
        MessageBox.Show("Будь ласка виберіть людину.");
        return;
      }
      _databaseManager.DeleteResident(_currentHotel, _roomNumber, person);
      _mainWindow.HotelDetailsItemsControl.Items.Refresh();
      _mainWindow.HotelRoomsItemsControl.Items.Refresh();
    }
  }
}
