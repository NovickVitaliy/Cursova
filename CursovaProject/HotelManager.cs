using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections.ObjectModel;
using CursovaProject.Rooms;
namespace CursovaProject
{
  internal class HotelManager
  {
    public ObservableCollection<Hotel> CreateHotelsObjectsBasedOnSavedData(string[] pathsToData, string[] names)
    {
      var list = new ObservableCollection<Hotel>();
      for (int i = 0; i < pathsToData.Length; i++)
      {
        var data = File.ReadAllLines(pathsToData[i] + "/" + names[i] + ".txt");
        string name = data[0].Substring(data[0].IndexOf(':') + 1);
        int stR = Convert.ToInt32(data[1].Substring(data[1].IndexOf(':') + 1));
        int srR = Convert.ToInt32(data[2].Substring(data[2].IndexOf(':') + 1));
        int prR = Convert.ToInt32(data[3].Substring(data[3].IndexOf(':') + 1));
        int priceStR = Convert.ToInt32(data[4].Substring(data[4].IndexOf(':') + 1));
        int priceSrR = Convert.ToInt32(data[5].Substring(data[5].IndexOf(':') + 1));
        int pricePrR = Convert.ToInt32(data[6].Substring(data[6].IndexOf(':') + 1));
        int totalIncome = Convert.ToInt32(data[10].Substring(data[10].IndexOf(':') + 1));
        Hotel hotel = new Hotel(name, stR, srR, prR, priceStR, priceSrR, pricePrR);
        hotel.TotalIncome = totalIncome;
        CreateRoomsBasedOnData(pathsToData[i], names[i], hotel);
        list.Add(hotel);
      }
      return list;
    }
    private void CreateRoomsBasedOnData(string pathToHotelsDatabase, string hotelName, Hotel hotel)
    {
      var directories = Directory.GetDirectories(pathToHotelsDatabase + "/");
      directories = directories.OrderBy(d => int.Parse((d.Split('/')).Last())).ToArray();
      for (int i = 0; i < directories.Length; i++)
      {
        var d = directories[i];
        var data = File.ReadAllLines(d + "/roomInfo.txt");
        string roomType = data[0].Substring(data[0].LastIndexOf(":") + 1);
        int roomNumber = Convert.ToInt32(data[1].Substring(data[1].IndexOf(":") + 1));
        DateTime dateIn = Convert.ToDateTime(data[2].Substring(data[2].IndexOf(':') + 1));
        DateTime dateOut = Convert.ToDateTime(data[3].Substring(data[3].IndexOf(':') + 1));
        bool isCleaned = Convert.ToBoolean(data[4].Substring(data[4].IndexOf(':') + 1));
        HotelRoom newlyAddedRoom = hotel.RegisterRoom(GetRoomType(roomType),dateIn, dateOut, roomNumber);
        newlyAddedRoom.IsCleaned = isCleaned;
        GetSavedResidents(pathToHotelsDatabase, hotel.HotelRooms.Last().RoomNumber, hotel);
      }
    }

    private void GetSavedResidents(string pathToHotelDatabase, int roomNumber, Hotel hotel)
    {
      string[] data;
      if (File.Exists(pathToHotelDatabase + "/" + roomNumber + "/" + "residents.txt"))
      {
        data = File.ReadAllLines(pathToHotelDatabase + "/" + roomNumber + "/" + "residents.txt");
        List<Person> persons = new List<Person>();
        for (int i = 0; i < data.Length; i += 6)
        {
          string name = data[i + 0].Substring(data[i + 0].LastIndexOf(":") + 1);
          string surname = data[i + 1].Substring(data[i + 1].LastIndexOf(":") + 1);
          string secondName = data[i + 2].Substring(data[i + 2].LastIndexOf(":") + 1);
          int age = Convert.ToInt32(data[i + 3].Substring(data[i + 3].LastIndexOf(":") + 1));
          int passportSeries = Convert.ToInt32(data[i + 4].Substring(data[i + 4].LastIndexOf(":") + 1));
          int passportNumber = Convert.ToInt32(data[i + 5].Substring(data[i + 5].LastIndexOf(":") + 1));
          persons.Add(new Person(name, secondName, surname, age, passportSeries, passportNumber));
        }
        foreach (var person in persons)
        {
          hotel.HotelRooms.Last().RegisterResident(person);
        }
      }
    }

    private RoomTypes GetRoomType(string roomType)
    {
      switch (roomType)
      {
        case "StandartRoom": return RoomTypes.StandartRoom;
        case "SuperiorRoom": return RoomTypes.SuperiorRoom;
        case "PresidentRoom": return RoomTypes.PresidentRoom;
        default: throw new Exception("Сталася непередбачувана помилка");
      }
    }
  }
}
