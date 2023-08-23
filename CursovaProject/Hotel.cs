﻿using CursovaProject.RoomFactory;
using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Xml.Linq;

namespace CursovaProject
{
    public class Hotel
    {
        private string _name;
        private int _numberOfStandartRooms;
        private int _numberOfSuperiorRooms;
        private int _numberOfPresidentRooms;
        private int _numberOfAvailableStandartRooms;
        private int _numberOfAvailableSuperiorRooms;
        private int _numberOfAvailablePresidentRooms;
        private string[] _roomTypes;
        private IRoomFactory _roomFactory;
        private int _priceStandartRoom;
        private int _priceSuperiorRooms;
        private int _pricePresidentRooms;
        private int _totalAmountOfRoom;
        private List<int> _roomNumbers;


        ObservableCollection<HotelRoom> _rooms;

        public Hotel(string name, int numberOfStandartRooms, int numberOfSuperiorRooms, int numberOfPresidentRooms,
            int priceStandartRoom, int priceSuperiorRoom, int pricePresidentRoom)
        {
            Name = name;
            NumberOfStandartRooms = numberOfStandartRooms;
            NumberOfSuperiorRooms = numberOfSuperiorRooms;
            NumberOfPresidentRooms = numberOfPresidentRooms;
            AvailableStandartRooms = numberOfStandartRooms;
            AvailableSuperiorRooms = numberOfSuperiorRooms;
            AvailablePresidentRooms = numberOfPresidentRooms;
            _priceStandartRoom = priceStandartRoom;
            _priceSuperiorRooms = priceSuperiorRoom;
            _pricePresidentRooms = pricePresidentRoom;
            _totalAmountOfRoom = numberOfStandartRooms + numberOfSuperiorRooms + numberOfPresidentRooms;
            _roomNumbers = new List<int>();
            SetListOfRoomNumbers(_totalAmountOfRoom);
            _rooms = new ObservableCollection<HotelRoom>();
            _roomTypes = new string[]
            {
                "Standart Room",
                "Superior Room",
                "President Room"
            };
        }

        public string Name { get => _name; set => _name = value; }
        public int NumberOfStandartRooms { get => _numberOfStandartRooms; private set => _numberOfStandartRooms = value; }
        public int NumberOfSuperiorRooms { get => _numberOfSuperiorRooms; private set => _numberOfSuperiorRooms = value; }
        public int NumberOfPresidentRooms { get => _numberOfPresidentRooms; private set => _numberOfPresidentRooms = value; }

        public ObservableCollection<HotelRoom> HotelRooms { get => _rooms; private set => _rooms = value; }



        public int AvailableStandartRooms { get => _numberOfAvailableStandartRooms; set => _numberOfAvailableStandartRooms = value; }

        public int AvailableSuperiorRooms { get => _numberOfAvailableSuperiorRooms; set => _numberOfAvailableSuperiorRooms = value; }
        public int AvailablePresidentRooms { get => _numberOfAvailablePresidentRooms; set => _numberOfAvailablePresidentRooms = value; }
        public int PriceStandartRoom { get => _priceStandartRoom; set => _priceStandartRoom = value; }
        public int PriceSuperiorRoom { get => _priceSuperiorRooms; set => _priceSuperiorRooms = value; }
        public int PricePresidentRoom { get => _pricePresidentRooms; set => _pricePresidentRooms = value; }

        public HotelRoom GetHotelRoom(int number)
        {
            return HotelRooms.Where(hr => hr.RoomNumber == number).FirstOrDefault();
        }

        public string[] RoomTypes
        {
            get
            {
                return _roomTypes;
            }
        }
        public string GetData()
        {
            return $"Готель: '{Name}'\n" +
                $"К-сть стандратних кімнат: {NumberOfStandartRooms}, Ціна за одну людину за ніч: {PriceStandartRoom}\n" +
                $" К-сть покращених кімнат: {NumberOfSuperiorRooms}, Ціна за одну людину за ніч: {PriceSuperiorRoom}\n" +
                $" К-сть президентських кімнат: {NumberOfPresidentRooms}, Ціна за одну людину за ніч: {PricePresidentRoom}";
        }

        public override string ToString()
        {
            return Name;
        }

        public void RegisterRoom(RoomTypes roomType, int roomNumber = -1)
        {
            DecreaseAvailableRooms(roomType);
            _roomFactory = GetFactory(roomType);
            if (roomNumber == -1)
                roomNumber = _roomNumbers.FirstOrDefault();
            _roomNumbers.Remove(roomNumber);
            _rooms.Add(_roomFactory.GetRoom(roomNumber));
        }

        private IRoomFactory GetFactory(RoomTypes roomType)
        {
            IRoomFactory factory;

            switch (roomType)
            {
                case CursovaProject.RoomTypes.StandartRoom:
                    factory = new StandartRoomFactory(PriceStandartRoom);
                    break;
                case CursovaProject.RoomTypes.SuperiorRoom:
                    factory = new SuperiorRoomFactory(PriceSuperiorRoom);
                    break;
                case CursovaProject.RoomTypes.PresidentRoom:
                    factory = new PresidentRoomFactory(PricePresidentRoom);
                    break;
                default:
                    factory = null;
                    break;
            }

            return factory;
        }


        private void DecreaseAvailableRooms(RoomTypes roomType)
        {
            switch (roomType)
            {
                case CursovaProject.RoomTypes.StandartRoom: AvailableStandartRooms--; break;
                case CursovaProject.RoomTypes.SuperiorRoom: AvailableSuperiorRooms--; break;
                case CursovaProject.RoomTypes.PresidentRoom: AvailablePresidentRooms--; break;
            }
        }

        public int GetAvailableRoomsBasedOnType(RoomTypes roomType)
        {
            switch (roomType)
            {
                case CursovaProject.RoomTypes.StandartRoom: return AvailableStandartRooms;
                case CursovaProject.RoomTypes.SuperiorRoom: return AvailableSuperiorRooms;
                case CursovaProject.RoomTypes.PresidentRoom: return AvailablePresidentRooms;
                default: return -1;
            }
        }

        public void DeleteRoom(int roomNumber)
        {
            var type = _rooms.FirstOrDefault(r => r.RoomNumber == roomNumber).RoomType;

            switch (type)
            {
                case "StandartRoom":
                    AvailableStandartRooms++;
                    break;
                case "SuperiorRoom":
                    AvailableSuperiorRooms++;
                    break;
                case "PresidentRoom":
                    AvailablePresidentRooms++;
                    break;
                default:
                    MessageBox.Show("wrong");
                    break;
            }

            _rooms.Remove(_rooms.FirstOrDefault(r => r.RoomNumber == roomNumber));
            _roomNumbers.Add(roomNumber);
            _roomNumbers.Sort();
        }

        private void SetListOfRoomNumbers(int total)
        {
            for (int i = 1; i <= total; ++i)
            {
                _roomNumbers.Add(i);
            }
        }

        public void SortByKey(string key)
        {
            ObservableCollection<HotelRoom> sortedCollection;
            switch (key)
            {
                case "NumberSortItem":
                    sortedCollection = new ObservableCollection<HotelRoom>(HotelRooms.OrderBy(r => r.RoomNumber));
                    HotelRooms = sortedCollection;
                    break;
                case "ResidentQuantitySortItem":
                    sortedCollection = new ObservableCollection<HotelRoom>(HotelRooms.OrderBy(r => r.Residents.Count));
                    HotelRooms = sortedCollection;
                    break;
                case "TypeOfRoomSortItem":
                    sortedCollection = new ObservableCollection<HotelRoom>(HotelRooms.OrderBy(r => r.RoomType));
                    HotelRooms = sortedCollection;
                    break;
            }
        }

        public int GetTotalAmountOfResidentInSpecificRoomType(string type)
        {
            type = String.Concat(type.Where(c => !Char.IsWhiteSpace(c)));

            switch (type)
            {
                case "StandartRoom":
                    return HotelRooms.Where(r => r.RoomType == type).Sum(r => r.Residents.Count);
                case "SuperiorRoom":
                    return HotelRooms.Where(r => r.RoomType == type).Sum(r => r.Residents.Count);
                case "PresidentRoom":
                    return HotelRooms.Where(r => r.RoomType == type).Sum(r => r.Residents.Count);
            }

            return 0;
        }

        public ObservableCollection<HotelRoom> GetRoomsFilteredRoomsBasedOnOption(string option, string RoomType = "")
        {
            {
                ObservableCollection<HotelRoom> filteredList;

                switch (option)
                {
                    case "ShowAllRooms":
                        return _rooms;
                    case "ShowOnlyRoomsWithResidents":
                        return new ObservableCollection<HotelRoom>(_rooms.Where(r => r.Residents.Count > 0));
                    case "ShowRoomsByType":
                        return new ObservableCollection<HotelRoom>(_rooms.Where(r => r.RoomType == RoomType));
                    default:
                        MessageBox.Show("wrong");
                        return new ObservableCollection<HotelRoom>();
                }
            }
        }
    }
}
