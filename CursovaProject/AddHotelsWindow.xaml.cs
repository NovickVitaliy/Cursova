using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for AddHotelsWindow.xaml
  /// </summary>
  public partial class AddHotelsWindow : Window
  {
    private MainWindow _mainWindow;
    public DatabaseManager _databaseManager;
    private ObservableCollection<Hotel> _hotelList;
    public AddHotelsWindow(MainWindow mainWindow, DatabaseManager databaseManager, ObservableCollection<Hotel> hotelList)
    {
      InitializeComponent();
      _mainWindow = mainWindow;
      _databaseManager = databaseManager;
      _hotelList = hotelList;
    }
    private void CreateHotel_ButtonOnClick(object sender, RoutedEventArgs e)
    {
      try
      {
        string name = tbHotelName.Text;
        int standartRooms = Convert.ToInt32(tbStandartRoomsCount.Text);
        int superiorRooms = Convert.ToInt32(tbSuperiorRoomsCount.Text);
        int presidentRooms = Convert.ToInt32(tbPresidentRoomsCount.Text);
        int stRPrice = Convert.ToInt32(tbPriceOnePersonStandartRoom.Text);
        int srRPrice = Convert.ToInt32(tbPriceOnePersonSuperiorRoom.Text);
        int prRPrice = Convert.ToInt32(tbPriceOnePersonPresidentRoom.Text);
        if (standartRooms < 0 || superiorRooms < 0 || presidentRooms < 0)
        {
          throw new NegativeValueException("Від'ємне значення не може бути використане для к-сті номерів");
        }
        if (stRPrice < 0 || srRPrice < 0 || prRPrice < 0)
        {
          throw new NegativeValueException("Від'ємн ціни не можуть бути використані для ціни номера за одну людину");
        }
        var wasCreated = _databaseManager.CreateDatabaseOfHotelIfNotExist(name, standartRooms, superiorRooms, presidentRooms, stRPrice, srRPrice, prRPrice);
        if (wasCreated)
        {
          _hotelList.Add(new Hotel(name, standartRooms, superiorRooms, presidentRooms, stRPrice, srRPrice, prRPrice));
          MessageBox.Show("Об'єкт готелю було успішно створено", "Створення готелю", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        else
        {
          MessageBox.Show("Об'єкт готелю не було створено оскільки такий готель вже існує", "Помилка при створенні готелю", MessageBoxButton.OK, MessageBoxImage.Error);
        }
      }
      catch (NegativeValueException nve)
      {
        MessageBox.Show(nve.Message, "Помилка форматування", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
      catch (FormatException)
      {
        MessageBox.Show("Значення кількості номерів не є числом або ціна за номер не була введена.", "Помилка форматування", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
      catch (OverflowException)
      {
        MessageBox.Show("Дані значення є завеликими для зберігання.", "Помилка форматування", MessageBoxButton.OK, MessageBoxImage.Exclamation);
      }
    }
    private void roomsCountChangedButton_OnClick(object sender, TextChangedEventArgs e)
    {
      var tb = (sender as TextBox);
      if (tb != null)
      {
        int value;
        bool doesValueEqualZero = false;
        bool didParse = Int32.TryParse(tb.Text, out value);
        if (didParse) doesValueEqualZero = value == 0;
        var name = tb.Name;
        if (doesValueEqualZero)
        {
          switch (name)
          {
            case "tbStandartRoomsCount":
              tbPriceOnePersonStandartRoom.Text = "0";
              tbPriceOnePersonStandartRoom.IsEnabled = false;
              break;
            case "tbSuperiorRoomsCount":
              tbPriceOnePersonSuperiorRoom.Text = "0";
              tbPriceOnePersonSuperiorRoom.IsEnabled = false;
              break;
            case "tbPresidentRoomsCount":
              tbPriceOnePersonPresidentRoom.Text = "0";
              tbPriceOnePersonPresidentRoom.IsEnabled = false;
              break;
            default:
              throw new Exception();
          }
        }
        else
        {
          switch (name)
          {
            case "tbStandartRoomsCount":
              tbPriceOnePersonStandartRoom.IsEnabled = true;
              break;
            case "tbSuperiorRoomsCount":
              tbPriceOnePersonSuperiorRoom.IsEnabled = true;
              break;
            case "tbPresidentRoomsCount":
              tbPriceOnePersonPresidentRoom.IsEnabled = true;
              break;
            default:
              throw new Exception("Щось пішло не так...");
          }
        }
      }
    }
  }
}
