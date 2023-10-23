using System;
using System.Windows;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for AddResidentWindow.xaml
  /// </summary>
  public partial class AddResidentWindow : Window
  {
    Hotel _currentHotel;
    int _roomNumber;
    DatabaseManager _databaseManager;
    MainWindow _mainWindow;
    public AddResidentWindow(Hotel currentHotel, int roomNumber, DatabaseManager databaseManager, MainWindow mainWindow)
    {
      InitializeComponent();
      _currentHotel = currentHotel;
      _roomNumber = roomNumber;
      _databaseManager = databaseManager;
      _mainWindow = mainWindow;
    }
    private void AddResidentButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        string name = tbName.Text;
        string surname = tbSurname.Text;
        string secondName = tbSecondName.Text;
        int passportSeries = Convert.ToInt32(tbPassportSeries.Text);
        int passportNumber = Convert.ToInt32(tbPassportNumber.Text);
        int age = Convert.ToInt32(tbAge.Text);
        if (age <= 0)
        {
          throw new NegativeValueException();
        }
        if (name == string.Empty || surname == string.Empty || secondName == string.Empty)
        {
          throw new ArgumentException();
        }
        if (passportNumber <= 0 || passportSeries <= 0)
        {
          throw new NegativeValueException();
        }
        Person person = new Person(name, secondName, surname, age, passportSeries, passportNumber);
        _databaseManager.SaveResident(_currentHotel.Name, _roomNumber, person);
        _currentHotel.GetHotelRoom(_roomNumber).RegisterResident(person);
        _mainWindow.HotelDetailsItemsControl.Items.Refresh();
        _mainWindow.HotelRoomsItemsControl.Items.Refresh();
        MessageBox.Show("Реєстрація відвідувача пройшла успішно", "Реєстрація відвідувача", MessageBoxButton.OK, MessageBoxImage.Information);
        Close();
      }
      catch (ArgumentException)
      {
        MessageBox.Show("Будь ласка, заповніть значення полів");
      }
      catch (FormatException)
      {
        MessageBox.Show($"Значення віку, номера або серії паспорта було введено не числовим значенням.");
      }
      catch (OverflowException)
      {
        MessageBox.Show("Неприпустиме значення для серії паспорта або номера");
      }
      catch (NegativeValueException)
      {
        MessageBox.Show("Значення віку, номера паспорт або серії не може бути від'ємним або рівними нулю.");
      }
    }
  }
}
