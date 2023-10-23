using CursovaProject.Rooms;
namespace CursovaProject.RoomFactory
{
    public interface IRoomFactory
    {
        HotelRoom GetRoom(int roomNumber);
    }
}
