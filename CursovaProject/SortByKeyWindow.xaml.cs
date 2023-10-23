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
        _chosenHotel.SortByKey(key);
        _itemsControl.ItemsSource = _chosenHotel.HotelRooms;
      }
      else
      {
        MessageBox.Show("Будь ласка, виберіть ключ для сортування", "Помилка при виборі ключа", MessageBoxButton.OK, MessageBoxImage.Error);
      }
    }
  }
}