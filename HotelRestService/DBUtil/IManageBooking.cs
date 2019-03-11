using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageBooking
    {
        List<Booking> GetAllBookings();

        Booking GetBookingFromID(int bookingId);

        bool CreateBooking(Booking booking);

        bool UpdateBooking(Booking booking, int bookingId);

        bool DeleteBooking(int bookingId);
    }
}
