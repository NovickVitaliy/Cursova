namespace CursovaProject.Rooms
{
  internal class PresidentRoom : HotelRoom
  {
    private float overPay = 1.5f;
    public PresidentRoom(int pricePerOnePersonPerDay, int roomNumber, RoomTypes roomType)
        : base(pricePerOnePersonPerDay, roomNumber, roomType) {}
    public override float GetTotalPrice
    {
      get { return PricePerOnePersonPerDay * Residents.Count * overPay; }
    }
    public override float GetTotalPriceInTheEnd()
    { return GetTotalPrice * (base.DateOfCheckOut - base.DateOfCheckIn).Days; }
  }
}