using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class GuestController : ApiController
    {
        ManageGuest _mngeGuest = new ManageGuest();

        public IEnumerable<Guest> Get()
        {
            return _mngeGuest.GetAllGuest();
        }
        
        public Guest Get(int id)
        {
            return _mngeGuest.GetGuestFromId(id);
        }
        
        public bool Post([FromBody]Guest value)
        {
            return _mngeGuest.CreateGuest(value);
        }
        
        public bool Put(int id, [FromBody]Guest value)
        {
            return _mngeGuest.UpdateGuest(value,id);
        }
        
        public bool Delete(int id)
        {
            return _mngeGuest.DeleteGuest(id);
        }
    }
}
