using CursovaProject.Rooms;
namespace CursovaProject.RoomFactory
{
    internal class SuperiorRoomFactory : IRoomFactory
    {
        private int _priceSuperiorRoom;
        public SuperiorRoomFactory(int pricePerOnePersonPerDay)
        {
            _priceSuperiorRoom = pricePerOnePersonPerDay;
        }
        public HotelRoom GetRoom(int roomNumber)
        {
            return new SuperiorRoom(_priceSuperiorRoom, roomNumber, RoomTypes.SuperiorRoom);
        }
    }
}
