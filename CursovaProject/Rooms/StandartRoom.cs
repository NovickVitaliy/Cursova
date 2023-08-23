using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject.Rooms
{
    internal class StandartRoom : HotelRoom
    {
        public StandartRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType) 
            : base(pricePerOnePersonPerDay, roomNumber, roomType)
        {
        }

        public override float GetTotalPrice
        {
            get
            {
                return PricePerOnePersonPerDay * Residents.Count;
            }
        }
    }
}
