using CursovaProject.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject.RoomFactory
{
    public interface IRoomFactory
    {
        HotelRoom GetRoom(int roomNumber);
    }
}
