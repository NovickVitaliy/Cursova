using CursovaProject.Rooms;
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
            if(RoomsComboBox.SelectedItem != null)
            {
                int roomNumber = (RoomsComboBox.SelectedItem as HotelRoom).RoomNumber;
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
