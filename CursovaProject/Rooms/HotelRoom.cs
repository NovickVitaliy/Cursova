using System.Collections.ObjectModel;

namespace CursovaProject.Rooms
{
    public abstract class HotelRoom
    {
        private int _pricePerOnePersonPerDay;
        private int _roomNumber;
        private RoomTypes _roomType;
        private ObservableCollection<Person> _persons;


        public HotelRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType)
        {
            _pricePerOnePersonPerDay = pricePerOnePersonPerDay;
            _roomNumber = roomNumber;
            _roomType = roomType;
            _persons = new ObservableCollection<Person>();

        }

        public abstract float GetTotalPrice { get; }



        public int PricePerOnePersonPerDay
        {
            get { return _pricePerOnePersonPerDay; }
        }

        public int RoomNumber
        {
            get { return _roomNumber; }
        }

        public int UniqueRoomNumber
        {
            get { return _roomNumber + 1000; }
        }
        public ObservableCollection<Person> Residents
        {
            get { return _persons; }
        }

        public string RoomType
        {
            get { return _roomType.ToString(); }

        }

        public void RegisterResident(Person person)
        {
            _persons.Add(person);
        }

        public string GetInfo()
        {
            return $"Кімната номер:{RoomNumber}\n" +
                $"Tип кімнати:{RoomType}\n" +
                $"Ціна за одну людину:{PricePerOnePersonPerDay}\n" +
                $"К-сть проживаючих:{Residents.Count}";
        }
    }
}
