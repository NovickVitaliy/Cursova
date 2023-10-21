using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for EditDataAboutHotel.xaml
  /// </summary>
  public partial class EditDataAboutHotel : Window
  {
    private DatabaseManager _databaseManager;
    private Hotel _chosenHotel;
    private MainWindow _mainWindow;
    public EditDataAboutHotel(DatabaseManager databaseManager, Hotel hotel, MainWindow mainWindow)
    {
      InitializeComponent();
      _databaseManager = databaseManager;
      _chosenHotel = hotel;
      _mainWindow = mainWindow;
    }

    private void cbEditOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (cbEditOption.SelectedIndex == 0)
      {
        cbRoomType.Visibility = Visibility.Collapsed;
      }
      else if (cbEditOption.SelectedIndex == 1 || cbEditOption.SelectedIndex == 2)
      {
        cbRoomType.Visibility = Visibility.Visible;
      }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      string value = tbNewValue.Text;

      if (value.Length == 0)
      {
        MessageBox.Show("Значення не може бути пустим.");
        return;
      }

      if (cbEditOption.SelectedIndex == 0)
      {
        _databaseManager.ChangeHotelName(_chosenHotel, value);
      }
      else if (cbEditOption.SelectedIndex == 1)
      {
        bool success = int.TryParse(tbNewValue.Text, out int newNumberOfRooms);
        if (!success || newNumberOfRooms < 0)
        {
          MessageBox.Show("Неможливо число для кількості кімнат");
          return;
        }
        string roomType = cbRoomType.Text;
        RoomTypes type = GetRoomType(roomType);

        if(_chosenHotel.HotelRooms.Where(room => room.GetType().Name.Equals(type.ToString())).Count() > newNumberOfRooms)
        {
          MessageBoxResult result = MessageBox.Show("Ви збираєтесь зменшити кількість наявних у вас кімнат. Ви можете втратити дані про людей які вже живуть у вашому готелі. Ви згідні?", "Видаленн кімнат", MessageBoxButton.YesNo);
          if(result == MessageBoxResult.Yes)
          {
            _databaseManager.ChangeNumberOfRooms(_chosenHotel, type, newNumberOfRooms);
          }
        }
        _databaseManager.ChangeNumberOfRooms(_chosenHotel, type, newNumberOfRooms);

      }
      else if (cbEditOption.SelectedIndex == 2)
      {
        bool success = int.TryParse(tbNewValue.Text, out int newPrice);
        if (!success || newPrice < 0)
        {
          MessageBox.Show("Неможливо число для ціни");
          return;
        }

        string roomType = cbRoomType.Text;
        RoomTypes type = GetRoomType(roomType);
        _databaseManager.ChangePriceOfRoom(_chosenHotel, type, newPrice);
      }


      _mainWindow.hotelControl.Items.Refresh();
      _mainWindow.HotelDetailsItemsControl.Items.Refresh();
      _mainWindow.HotelRoomsItemsControl.Items.Refresh();
    }

    private RoomTypes GetRoomType(string type)
    {
      switch (type)
      {
        case "Стандартний номер":
          return RoomTypes.StandartRoom;
        case "Покращений номер":
          return RoomTypes.SuperiorRoom;
        case "Президентський номер":
          return RoomTypes.PresidentRoom;
        default:
          return RoomTypes.StandartRoom;
      }
    }
  }
}
