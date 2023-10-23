using CursovaProject.Rooms;
namespace CursovaProject.RoomFactory
{
    internal class PresidentRoomFactory : IRoomFactory
    {
        private int _pricePresidentRoom;
        public PresidentRoomFactory(int pricePerOnePersonPerDay)
        {
            _pricePresidentRoom = pricePerOnePersonPerDay;
        }
        public HotelRoom GetRoom(int roomNumber)
        {
            return new PresidentRoom(_pricePresidentRoom, roomNumber, RoomTypes.PresidentRoom);
        }
    }
}
