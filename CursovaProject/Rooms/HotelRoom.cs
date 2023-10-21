using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CursovaProject.Rooms
{
  public abstract class HotelRoom : INotifyPropertyChanged
  {
    private int _pricePerOnePersonPerDay;
    private int _roomNumber;
    private DateTime _dateOfCheckIn;
    private DateTime _dateOfCheckOut;
    private RoomTypes _roomType;
    private ObservableCollection<Person> _persons;
    public static readonly int MAX_AMOUNT_OF_RESIDENTS = 4;
    private int _currentAmountOfResident = 0;

    public event PropertyChangedEventHandler PropertyChanged;

    public HotelRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType)
    {
      _pricePerOnePersonPerDay = pricePerOnePersonPerDay;
      _roomNumber = roomNumber;
      _roomType = roomType;
      _persons = new ObservableCollection<Person>();

    }

    public abstract float GetTotalPrice { get; }

    public DateTime DateOfCheckIn
    { 
      get { return _dateOfCheckIn; }
      set { _dateOfCheckIn = value; }
    }

    public DateTime DateOfCheckOut
    {
      get { return _dateOfCheckOut; }
      set { _dateOfCheckOut = value; }
    }


    public int PricePerOnePersonPerDay
    {
      get 
      { 
        return _pricePerOnePersonPerDay; 
      }
      set
      {
        _pricePerOnePersonPerDay = value;
        OnPropertyChanged();
      }
    }

    public int RoomNumber
    {
      get 
      { 
        return _roomNumber; 
      }
      set
      {
        _roomNumber = value;
        OnPropertyChanged();
      }
    }

    public int UniqueRoomNumber
    {
      get { return _roomNumber + 1000; }
    }
    public ObservableCollection<Person> Residents
    {
      get 
      { 
        return _persons; 
      }
      set
      {
        _persons = value;
        OnPropertyChanged();
      }
    }

    public string RoomType
    {
      get { return _roomType.ToString(); }

    }

    public void RegisterResident(Person person)
    {
      _persons.Add(person);
      _currentAmountOfResident++;
      OnPropertyChanged();
    }

    public int NumberOfResidents
    {
      get { return _currentAmountOfResident; }
      set { _currentAmountOfResident = value; OnPropertyChanged(); }
    }

    public string GetInfo()
    {
      return $"Кімната номер:{RoomNumber}\n" +
          $"Tип кімнати:{RoomType}\n" +
          $"Ціна за одну людину:{PricePerOnePersonPerDay}\n" +
          $"К-сть проживаючих:{Residents.Count}";
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
  }
}
