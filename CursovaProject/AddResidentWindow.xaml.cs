using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        if (name == string.Empty || surname == string.Empty || secondName == string.Empty)
        {
          throw new ArgumentException();
        }

        if (passportNumber <= 0 || passportSeries <= 0)
        {
          throw new NegativeValueException("Значення номера паспорт або серії не може бути від'ємним або рівними нулю.");
        }

        Person person = new Person(name, secondName, surname, passportSeries, passportNumber);
        _databaseManager.SaveResident(_currentHotel.Name, _roomNumber, person);
        _currentHotel.GetHotelRoom(_roomNumber).RegisterResident(person);
        _mainWindow.HotelDetailsItemsControl.Items.Refresh();
        _mainWindow.HotelRoomsItemsControl.Items.Refresh();

        Close();
      }
      catch (ArgumentException)
      {
        MessageBox.Show("Будь ласка, заповніть значення полів");
      }
      catch (FormatException)
      {
        MessageBox.Show($"Значення номера або серії паспорта було введено не числовим значенням.");
      }
      catch (OverflowException)
      {
        MessageBox.Show("Неприпустиме значення для серії паспорта або номера");
      }
    }
  }
}
