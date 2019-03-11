using System;

namespace HotelModel
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int Hotel_No { get; set; }
        public int Guest_No { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public int Room_No { get; set; }
    }
}
