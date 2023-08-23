using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            if(RoomsComboBox.SelectedItem != null)
            {
                string chosenValue = RoomsComboBox.SelectedItem.ToString();
                RoomTypes roomType = GetRoomType(chosenValue);

                if (_hotel.GetAvailableRoomsBasedOnType(roomType) > 0)
                {
                    _hotel.RegisterRoom(roomType);
                    var room = _hotel.HotelRooms.Last();
                    _databaseManager.SaveHotelRoom(room.RoomNumber, room.RoomType, _hotel);
                }
                else
                {
                    MessageBox.Show($"Наразі, усі номери типу - {roomType.ToString()} заняті.");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть тип номеру", "Помилка при виборы типу номера", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private RoomTypes GetRoomType(string value)
        {
            switch(value)
            {
                case "Standart Room": return RoomTypes.StandartRoom;
                case "Superior Room": return RoomTypes.SuperiorRoom;
                case "President Room": return RoomTypes.PresidentRoom;
                default: throw new Exception("Сталася непередбачувана помилка");
            }
        }
    }
}
