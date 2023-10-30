using CursovaProject.Rooms;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for SortByKeyWindow.xaml
  /// </summary>
  public partial class SortByKeyWindow : Window
  {
    Hotel _chosenHotel;
    ItemsControl _itemsControl;
    public SortByKeyWindow(Hotel chosenHotel, ItemsControl itemsControl)
    {
      InitializeComponent();
      _chosenHotel = chosenHotel;
      _itemsControl = itemsControl;
    }
    private void SortByKey_Click(object sender, RoutedEventArgs e)
    {
      if (SortKeys.SelectedItem != null)
      {
        var item = SortKeys.SelectedItem as ComboBoxItem;
        string key = item.Name;
        var sorted = _chosenHotel.SortByKey((ObservableCollection<HotelRoom>)_itemsControl.ItemsSource, key);
        _itemsControl.ItemsSource = sorted;

      }
      else
      {
        MessageBox.Show("Будь ласка, виберіть ключ для сортування", "Помилка при виборі ключа", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}