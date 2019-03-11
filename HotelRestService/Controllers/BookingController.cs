using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class BookingController : ApiController
    {
        ManageBooking _mngBooking = new ManageBooking();
        public IEnumerable<Booking> Get()
        {
            return _mngBooking.GetAllBookings();
        }
        
        public Booking Get(int id)
        {
            return _mngBooking.GetBookingFromID(id);
        }
        
        public bool Post([FromBody]Booking value)
        {
            return _mngBooking.CreateBooking(value);
        }

        public bool Put([FromBody]Booking value, int id)
        {
            return _mngBooking.UpdateBooking(value, id);
        }
        
        public bool Delete(int id)
        {
            return _mngBooking.DeleteBooking(id);
        }
    }
}
