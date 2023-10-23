namespace CursovaProject.Rooms
{
  internal class StandartRoom : HotelRoom
  {
    public StandartRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType)
        : base(pricePerOnePersonPerDay, roomNumber, roomType) { }
    public override float GetTotalPrice
    {
      get { return PricePerOnePersonPerDay * Residents.Count; }
    }
    public override float GetTotalPriceInTheEnd()
    { return GetTotalPrice * (base.DateOfCheckOut - base.DateOfCheckIn).Days; }
  }
}