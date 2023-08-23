using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CursovaProject
{
    public class DatabaseManager
    {

        readonly string pathToHotels = Environment.CurrentDirectory + @"../../../HotelsDatabases/";
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
        public  string[] GetDatabasesPaths()
        {
            if(Directory.Exists(pathToHotels))
            {
                return Directory.GetDirectories(pathToHotels);
            }
            else
            {
                MessageBox.Show("Не вдалося знайти шлях до папки з записами про готелі.");
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
        public  bool CreateDatabaseOfHotelIfNotExist(string name, int standartRooms, int superiorRooms, int presidentRooms,
            int priceStandartRoom, int priceSuperiorRoom, int pricePresidentRoom)
        {
            if(!Directory.Exists(pathToHotels + name + "/"))
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
        public  string[] GetNamesOfDatabases(string[] databasesPaths)
        {
            string[] names = new string[databasesPaths.Length];

            for(int i = 0; i < databasesPaths.Length; i++)
            {
                string name = databasesPaths[i].Substring(databasesPaths[i].LastIndexOf('/') + 1);
                //name = name.Substring(0, name.LastIndexOf("."));
                names[i] = name;
            }

            return names;
        }

        
        public  void DeleteDatabase(string hotelName)
        {
            if (Directory.Exists(pathToHotels))
            {
                Directory.Delete(pathToHotels + hotelName, true);
            }
        }

        public void SaveHotelRoom(int roomNumber, string roomType, Hotel hotel)
        {
            string path = pathToHotels + hotel.Name + "/";
            if (Directory.Exists(path))
            {
                if(!Directory.Exists(path + roomNumber))
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
                stream.WriteLine($"residentPassportSeries:{person.PassortSeries}");
                stream.WriteLine($"residentPassportNumber:{person.PassportNumber}");
            }
        }

        public void DeleteRoom(Hotel chosenHotel, int roomNumber)
        {
            try
            {
                if(Directory.Exists(pathToHotels + chosenHotel.Name + "/" + roomNumber))
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
        #endregion
    }
}
