using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject.RoomFactory
{
    internal class StandartRoomFactory : IRoomFactory
    {
        private int _priceStandartRoom;
        public StandartRoomFactory(int priceStandartRoom) 
        { 
            _priceStandartRoom = priceStandartRoom;
        }

        public HotelRoom GetRoom(int roomNumber)
        {
            return new StandartRoom(_priceStandartRoom, roomNumber, RoomTypes.StandartRoom);
        }
    }
}
