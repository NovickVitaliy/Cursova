using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject.Rooms
{
    internal class SuperiorRoom : HotelRoom
    {
        private float overPay = 1.25f;
        public SuperiorRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType) 
            : base(pricePerOnePersonPerDay, roomNumber, roomType)
        {
        }

        public override float GetTotalPrice
        {
            get
            {
                return PricePerOnePersonPerDay * Residents.Count * overPay;
            }
        }
    }
}
