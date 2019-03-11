using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class HotelController : ApiController
    {
        ManageHotel _mngHotel = new ManageHotel();
        
        public IEnumerable<Hotel> Get()
        {
            return _mngHotel.GetAllHotel();
        }
        
        public Hotel Get(int id)
        {
            return _mngHotel.GetHotelFromId(id);
        }
        
        public bool Post([FromBody]Hotel value)
        {
            return _mngHotel.CreateHotel(value);
        }
        
        public bool Put(int id, [FromBody]Hotel value)
        {
            return _mngHotel.UpdateHotel(value, id);
        }
        
        public bool Delete(int id)
        {
            return _mngHotel.DeleteHotel(id);
        }
    }
}
