using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageRoom
    {
        List<Room> GetAllRooms();

        Room GetRoomFromID(int roomNr, int hotelNr);

        bool CreateRoom(Room room);

        bool UpdateRoom(Room room, int roomNr, int hotelNr);

        bool DeleteRoom(int roomNr, int hotelNr);
    }
}
