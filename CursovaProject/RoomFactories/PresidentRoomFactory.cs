using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
