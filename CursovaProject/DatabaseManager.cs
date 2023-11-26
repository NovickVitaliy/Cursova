using CursovaProject.Rooms;
using CursovaProject.Useful_Extensions;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace CursovaProject
{
  public class DatabaseManager
  {
    private readonly string pathToHotels = Environment.CurrentDirectory + @"../../../HotelsDatabases/";
    private ObservableCollection<Hotel> _hotelList;
    #region Methods
    public DatabaseManager(ref ObservableCollection<Hotel> hotelList)
    {
      _hotelList = hotelList;
    }
    /// <summary>
    /// Method to get all databases
    /// </summary>
    /// <returns>All databases of hotels</returns>
    public string[] GetDatabasesPaths()
    {
      if (Directory.Exists(pathToHotels))
      {
        return Directory.GetDirectories(pathToHotels);
      }
      else
      {
        Directory.CreateDirectory(pathToHotels);
      }
      return new string[0];
    }
    /// <summary>
    /// Creates database of hotel based on given data
    /// </summary>
    /// <param name="name"></param>
    /// <param name="standartRooms"></param>
    /// <param name="superiorRooms"></param>
    /// <param name="presidentRooms"></param>
    /// <returns>Whether database was created or not</returns>
    public bool CreateDatabaseOfHotelIfNotExist(string name, int standartRooms, int superiorRooms, int presidentRooms,
        int priceStandartRoom, int priceSuperiorRoom, int pricePresidentRoom)
    {
      if (!Directory.Exists(pathToHotels + name + "/"))
      {
        var directory = Directory.CreateDirectory(pathToHotels + name);
        if (!File.Exists(pathToHotels + name + "/" + name + ".txt"))
        {
          var hotel = new Hotel(name, standartRooms, superiorRooms, presidentRooms, priceStandartRoom,
              priceSuperiorRoom, pricePresidentRoom);

          using (StreamWriter streamWriter = new StreamWriter
              (pathToHotels + name + "/" + name + ".txt"))
          {
            streamWriter.WriteLine($"name:{name}");
            streamWriter.WriteLine($"standartRooms:{standartRooms}");
            streamWriter.WriteLine($"superiorRooms:{superiorRooms}");
            streamWriter.WriteLine($"presidetnRooms:{presidentRooms}");
            streamWriter.WriteLine($"priceStandartRoom:{priceStandartRoom}");
            streamWriter.WriteLine($"priceSuperiorRoom:{priceSuperiorRoom}");
            streamWriter.WriteLine($"pricePresidentRoom:{pricePresidentRoom}");
            streamWriter.WriteLine($"availableStandartRooms:{standartRooms}");
            streamWriter.WriteLine($"availableSuperiorRooms:{superiorRooms}");
            streamWriter.WriteLine($"availablePresidentRooms:{presidentRooms}");
            streamWriter.WriteLine($"totalIncome:{0}");
          }
          return true;
        }
        else
        {
          MessageBox.Show("Такий готель вже існує.");
        }
      }
      return false;
    }
    /// <summary>
    /// Return names of databases based in given pathToHotels
    /// </summary>
    /// <param name="databasesPaths"></param>
    /// <returns></returns>
    public string[] GetNamesOfDatabases(string[] databasesPaths)
    {
      string[] names = new string[databasesPaths.Length];
      for (int i = 0; i < databasesPaths.Length; i++)
      {
        string name = databasesPaths[i].Substring(databasesPaths[i].LastIndexOf('/') + 1);
        //name = name.Substring(0, name.LastIndexOf("."));
        names[i] = name;
      }
      return names;
    }
    public void DeleteDatabase(string hotelName)
    {
      if (Directory.Exists(pathToHotels))
      {
        Directory.Delete(pathToHotels + hotelName, true);
      }
    }
    public void ChangeDatesOfRoom(Hotel hotel, int roomNumber, DateTime newDateIn, DateTime newDateOut)
    {
      if(File.Exists(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt"))
      {
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt");
        data[2] = $"dateOfIn:{newDateIn}";
        data[3] = $"dateOfOut:{newDateOut}";
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt", data);
      }
      else
      {
        MessageBox.Show("Wrong");
      }
    }
    public void SaveHotelRoom(int roomNumber, string roomType, Hotel hotel, DateTime dateIn, DateTime dateOut)
    {
      string path = pathToHotels + hotel.Name + "/";
      if (Directory.Exists(path))
      {
        if (!Directory.Exists(path + roomNumber))
        {
          Directory.CreateDirectory(path + roomNumber);
          var file = File.Create(path + roomNumber + "/roomInfo.txt");
          file.Dispose();
        }
        if (File.Exists(path + roomNumber + "/roomInfo.txt"))
        {
          using (var streamWriter = new StreamWriter(path + roomNumber + "/roomInfo.txt"))
          {
            streamWriter.WriteLine($"roomType:{roomType}");
            streamWriter.WriteLine($"roomNumber:{roomNumber}");
            streamWriter.WriteLine($"dateOfIn:{dateIn}");
            streamWriter.WriteLine($"dateOfOut:{dateOut}");
            streamWriter.WriteLine($"isCleaned:{false}");
            ChangeAvailableRooms(hotel.Name, roomType, false);
          }
        }
      }
    }

    private void ChangeAvailableRooms(string hotelName, string roomType, bool increase)
    {
      var pathToHotelDataFile = Directory.GetDirectories(pathToHotels)
          .Where(folder => folder.Contains(hotelName))
          .FirstOrDefault();
      int value = -1;
      string[] data = File.ReadAllLines(pathToHotelDataFile + "/" + hotelName + ".txt");
      int number;
      switch (roomType)
      {
        case "StandartRoom":
          number = Convert.ToInt32(data[7].Substring(data[7].LastIndexOf(':') + 1)) - (increase == true ? value : 1);
          data[7] = $"availableStandartRooms:{number}";
          break;
        case "SuperiorRoom":
          number = Convert.ToInt32(data[8].Substring(data[8].LastIndexOf(':') + 1)) - (increase == true ? value : 1);
          data[8] = $"availableSuperiorRooms:{number}";
          break;
        case "PresidentRoom":
          number = Convert.ToInt32(data[9].Substring(data[9].LastIndexOf(':') + 1)) - (increase == true ? value : 1);
          data[9] = $"availablePresidentRooms:{number}";
          break;
        default:
          MessageBox.Show("wrong");
          break;
      }
      File.WriteAllLines(pathToHotelDataFile + "/" + hotelName + ".txt", data);
    }
    public void SaveResident(string hotelName, int roomNumber, Person person)
    {
      using (StreamWriter stream = new StreamWriter(pathToHotels + hotelName + "/" + roomNumber + "/" + "residents.txt", true))
      {
        stream.WriteLine($"residentName:{person.Name}");
        stream.WriteLine($"residentSurname:{person.Surname}");
        stream.WriteLine($"residentSecondName:{person.SecondName}");
        stream.WriteLine($"residentAge:{person.Age}");
        stream.WriteLine($"residentPassportSeries:{person.PassortSeries}");
        stream.WriteLine($"residentPassportNumber:{person.PassportNumber}");
      }
    }

    public void DeleteRoom(Hotel chosenHotel, int roomNumber)
    {
      try
      {
        if (Directory.Exists(pathToHotels + chosenHotel.Name + "/" + roomNumber))
        {
          string[] data = File.ReadAllLines(pathToHotels + chosenHotel.Name +
              "/" + roomNumber + "/" + "roomInfo.txt");
          string roomType = data[0].Substring(data[0].IndexOf(":") + 1);
          ChangeAvailableRooms(chosenHotel.Name, roomType, true);
          Directory.Delete(pathToHotels + chosenHotel.Name + "/" + roomNumber, true);
        }
        else
        {
          System.Windows.Forms.MessageBox.Show("Wrong");
        }
      }
      catch (Exception)
      {
        throw;
      }
    }


    public void ChangeHotelName(Hotel hotel, string newName)
    {
      if (Directory.Exists(pathToHotels + hotel.Name))
      {
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt");
        data[0] = data[0].Substring(0, data[0].IndexOf(':') + 1) + newName;
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt", data);
        File.Move(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt", pathToHotels + hotel.Name + "/" + newName + ".txt");
        Directory.Move(pathToHotels + hotel.Name, pathToHotels + newName);
        hotel.Name = newName;
      }
      else
      {
        MessageBox.Show("Шлях не знайдений. Перевірте чи знаходиться готель в потрібній папці");
      }
    }
    public void ChangePriceOfRoom(Hotel hotel, RoomTypes roomType, int newPrice)
    {
      if (Directory.Exists(pathToHotels + hotel.Name))
      {
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt");
        switch (roomType)
        {
          case RoomTypes.StandartRoom:
            ChangePrice(nameof(RoomTypes.StandartRoom), hotel, newPrice);
            data[4] = data[4].Substring(0, data[4].IndexOf(':') + 1) + newPrice;
            break;
          case RoomTypes.SuperiorRoom:
            ChangePrice(nameof(RoomTypes.SuperiorRoom), hotel, newPrice);
            data[5] = data[5].Substring(0, data[5].IndexOf(':') + 1) + newPrice;
            break;
          case RoomTypes.PresidentRoom:
            ChangePrice(nameof(RoomTypes.PresidentRoom), hotel, newPrice);
            data[6] = data[6].Substring(0, data[6].IndexOf(':') + 1) + newPrice;
            break;
        }
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt", data);
      }
      else
      {
        MessageBox.Show("Шлях не знайдений. Перевірте чи знаходиться готель в потрібній папці");
      }
    }
    public void ChangeNumberOfRooms(Hotel hotel, RoomTypes roomType, int newNumberOfRooms)
    {
      if (Directory.Exists(pathToHotels + hotel.Name))
      {
        int roomsWithResidents = 0;
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt");
        int roomToDelete = hotel.HotelRooms.Where(room => room.RoomType.Equals(roomType.ToString())).Count() - newNumberOfRooms;
        switch (roomType)
        {
          case RoomTypes.StandartRoom:
            if (newNumberOfRooms > hotel.HotelRooms.Where(hotelRoom => hotelRoom.GetType().Name.Equals(nameof(StandartRoom))).Count())
            {
              var oldQuantityOfSetRooms = hotel.NumberOfStandartRooms - hotel.AvailableStandartRooms;
              hotel.NumberOfStandartRooms = newNumberOfRooms;
              hotel.AvailableStandartRooms = newNumberOfRooms - oldQuantityOfSetRooms;
              data[1] = data[1].Substring(0, data[1].IndexOf(':') + 1) + newNumberOfRooms;
              data[7] = data[7].Substring(0, data[7].IndexOf(":") + 1) + hotel.AvailableStandartRooms;
            }
            else
            {
              roomsWithResidents = hotel.NumberOfStandartRooms - hotel.AvailableStandartRooms;
              //hotel.AvailableStandartRooms = newNumberOfRooms - hotel.AvailableStandartRooms;
              hotel.AvailableStandartRooms = newNumberOfRooms - roomsWithResidents;
              hotel.NumberOfStandartRooms = newNumberOfRooms;
              data[1] = data[1].Substring(0, data[1].IndexOf(':') + 1) + newNumberOfRooms;
              foreach (var item in hotel.HotelRooms.OrderByDescending(item => item.RoomNumber))
              {
                if (roomToDelete == 0)
                {
                  break;
                }
                var blabla = item.GetType().Name;
                if (blabla.Equals(roomType.ToString()))
                {
                  hotel.DeleteRoom(item.RoomNumber);
                  DeleteRoom(hotel, item.RoomNumber);
                  roomToDelete--;
                }
              }
            }
            break;
          case RoomTypes.SuperiorRoom:
            if (newNumberOfRooms > hotel.HotelRooms.Where(hotelRoom => hotelRoom.GetType().Name.Equals(nameof(SuperiorRoom))).Count())
            {
              var oldQuantityOfSetRooms = hotel.NumberOfSuperiorRooms - hotel.AvailableSuperiorRooms;
              hotel.NumberOfSuperiorRooms = newNumberOfRooms;
              hotel.AvailableSuperiorRooms = newNumberOfRooms - oldQuantityOfSetRooms;
              data[2] = data[2].Substring(0, data[2].IndexOf(':') + 1) + newNumberOfRooms;
              data[8] = data[8].Substring(0, data[8].IndexOf(":") + 1) + hotel.AvailableSuperiorRooms;
            }
            else
            {
              roomsWithResidents = hotel.NumberOfSuperiorRooms - hotel.AvailableSuperiorRooms;
              //hotel.AvailableStandartRooms = newNumberOfRooms - hotel.AvailableStandartRooms;
              hotel.AvailableSuperiorRooms = newNumberOfRooms - roomsWithResidents;
              hotel.NumberOfSuperiorRooms = newNumberOfRooms;
              data[2] = data[2].Substring(0, data[2].IndexOf(':') + 1) + newNumberOfRooms;
              foreach (var item in hotel.HotelRooms.OrderByDescending(item => item.RoomNumber))
              {
                if (roomToDelete == 0)
                {
                  break;
                }
                var blabla = item.GetType().Name;
                if (blabla.Equals(roomType.ToString()))
                {
                  hotel.DeleteRoom(item.RoomNumber);
                  DeleteRoom(hotel, item.RoomNumber);
                  roomToDelete--;
                }
              }
            }
            break;
          case RoomTypes.PresidentRoom:
            if (newNumberOfRooms > hotel.HotelRooms.Where(hotelRoom => hotelRoom.GetType().Name.Equals(nameof(PresidentRoom))).Count())
            {
              var oldQuantityOfSetRooms = hotel.NumberOfPresidentRooms - hotel.AvailablePresidentRooms;
              hotel.NumberOfPresidentRooms = newNumberOfRooms;
              hotel.AvailablePresidentRooms = newNumberOfRooms - oldQuantityOfSetRooms;
              data[3] = data[3].Substring(0, data[3].IndexOf(':') + 1) + newNumberOfRooms;
              data[9] = data[9].Substring(0, data[9].IndexOf(":") + 1) + hotel.AvailablePresidentRooms;
            }
            else
            {
              roomsWithResidents = hotel.NumberOfPresidentRooms - hotel.AvailablePresidentRooms;
              //hotel.AvailableStandartRooms = newNumberOfRooms - hotel.AvailableStandartRooms;
              hotel.AvailablePresidentRooms = newNumberOfRooms - roomsWithResidents;
              hotel.NumberOfPresidentRooms = newNumberOfRooms;
              data[3] = data[3].Substring(0, data[3].IndexOf(':') + 1) + newNumberOfRooms;
              foreach (var item in hotel.HotelRooms.OrderByDescending(item => item.RoomNumber))
              {
                if (roomToDelete == 0)
                {
                  break;
                }
                var blabla = item.GetType().Name;
                if (blabla.Equals(roomType.ToString()))
                {
                  hotel.DeleteRoom(item.RoomNumber);
                  DeleteRoom(hotel, item.RoomNumber);
                  roomToDelete--;
                }
              }
            }
            break;
        }
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + hotel.Name + ".txt", data);
      }
      else
      {
        MessageBox.Show("Шлях не знайдений. Перевірте чи знаходиться готель в потрібній папці");
      }
    }
    public void DeleteResident(Hotel hotel, int roomNumber, Person residentToDelete)
    {
      hotel.DeleteResident(roomNumber, residentToDelete);
      string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/" + "residents.txt");
      var dataAsList = data.ToList();
      int? indexOfSearchedItems = dataAsList.IndexOfItemBasedOnPredicate(item => item.Contains(residentToDelete.PassportNumber.ToString()));
      if (!indexOfSearchedItems.HasValue)
      {
        return;
      }
      dataAsList.RemoveRange(indexOfSearchedItems.Value - 5, 5);
      File.WriteAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/" + "residents.txt", dataAsList);
    }
    private void ChangePrice(string roomType, Hotel hotel, int newPrice)
    {
      foreach (var room in hotel.HotelRooms)
      {
        if (room.GetType().Name.Equals(roomType))
        {
          room.PricePerOnePersonPerDay = newPrice;
        }
      }
    }

    public void ChangeRoomCleanedStatus(Hotel hotel, int roomNumber, bool isCleaned)
    {
      if (File.Exists(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt"))
      {
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt");
        data[4] = $"isCleaned:{isCleaned}";
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + roomNumber + "/roomInfo.txt", data);
      }
      else
      {
        MessageBox.Show("Wrong");
      }
    }
    public void ChangeTotalIncome(Hotel hotel)
    {
      if (File.Exists(pathToHotels + hotel.Name + "/" + $"{hotel.Name}.txt"))
      {
        string[] data = File.ReadAllLines(pathToHotels + hotel.Name + "/" + $"{hotel.Name}.txt");
        data[10] = $"totalIncome:{hotel.TotalIncome}";
        File.WriteAllLines(pathToHotels + hotel.Name + "/" + $"{hotel.Name}.txt", data);
      }
      else
      {
        MessageBox.Show("Wrong");
      }
    }
    #endregion
  }
}
