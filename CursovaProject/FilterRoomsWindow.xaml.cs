using System;
using System.Windows;
using System.Windows.Controls;

namespace CursovaProject
{
    /// <summary>
    /// Interaction logic for FilterRoomsWindow.xaml
    /// </summary>
    public partial class FilterRoomsWindow : Window
    {
        Hotel _chosenHotel;
        MainWindow _mainWindow;
        public FilterRoomsWindow(Hotel chosenHotel, MainWindow mainWindow)
        {
            InitializeComponent();
            _chosenHotel = chosenHotel;
            _mainWindow = mainWindow;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string roomType;
                if(FilterOptionsComboBox.SelectedItem != null)
                {
                    string option = (FilterOptionsComboBox.SelectedItem as ComboBoxItem).Name;
                    if (option == "ShowRoomsByType")
                    {
                        if (RoomTypesCombobox.SelectedItem != null)
                        {
                            roomType = (RoomTypesCombobox.SelectedItem as ComboBoxItem).Name;
                            _mainWindow.HotelRoomsItemsControl.ItemsSource = _chosenHotel.GetRoomsFilteredRoomsBasedOnOption(option, roomType);
                        }
                        else
                        {
                            throw new ArgumentNullException("roomType");
                        }
                    }
                    else
                    {
                        _mainWindow.HotelRoomsItemsControl.ItemsSource = _chosenHotel.GetRoomsFilteredRoomsBasedOnOption(option);
                    }
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch(ArgumentException ae)
            {
                MessageBox.Show($"Будь ласка виберіть {(ae.ParamName == "roomType" ? "тип кімнати" : "опцію для фiльтрування")}");
            }
        }

        private void FilterOptionsComboBox_Selected(object sender, RoutedEventArgs e)
        {
            RoomTypesCombobox.Visibility = Visibility.Collapsed;
            try
            {
                if ((FilterOptionsComboBox.SelectedItem as ComboBoxItem).Name == "ShowRoomsByType")
                    RoomTypesCombobox.Visibility = Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("wrong 2");
            }
        }
    }
}
