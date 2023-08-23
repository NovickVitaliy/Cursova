using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
