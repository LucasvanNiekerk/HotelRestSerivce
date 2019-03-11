using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class RoomController : ApiController
    {
        private readonly ManageRoom _mngRoom = new ManageRoom();
        
        public IEnumerable<Room> Get()
        {
            return _mngRoom.GetAllRooms();
        }
        
        public Room Get(int roomNr, int hotelNr)
        {
            return _mngRoom.GetRoomFromID(roomNr, hotelNr);
        }
        
        public bool Post([FromBody]Room value)
        {
            return _mngRoom.CreateRoom(value);
        }
        
        public bool Put([FromBody]Room value, int roomNr, int hotelNr)
        {
            return _mngRoom.UpdateRoom(value, roomNr, hotelNr);
        }
        
        public bool Delete(int roomNr, int hotelNr)
        {
            return _mngRoom.DeleteRoom(roomNr, hotelNr);
        }
    }
}
