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
    /// Interaction logic for TotalNumbersOfResidentsInSpecificRoomType.xaml
    /// </summary>
    public partial class TotalNumbersOfResidentsInSpecificRoomType : Window
    {
        Hotel _chosenHotel;
        public TotalNumbersOfResidentsInSpecificRoomType(Hotel chosenHotel)
        {
            InitializeComponent();
            _chosenHotel = chosenHotel;
            RomTypesComboBox.ItemsSource = _chosenHotel.RoomTypes;
        }

        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if(RomTypesComboBox.SelectedItem != null)
            {
                string type = RomTypesComboBox.SelectedItem as string;
                int amount = _chosenHotel.GetTotalAmountOfResidentInSpecificRoomType(type);
                MessageBox.Show($"Загальна кількість проживаючих в кімнатах типу {type} становить {amount} людей");
            }
            else
            {
                MessageBox.Show("", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
