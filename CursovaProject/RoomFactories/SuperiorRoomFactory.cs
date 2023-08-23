using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
