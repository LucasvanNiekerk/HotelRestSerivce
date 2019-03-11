using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageHotel
    {
        List<Hotel> GetAllHotel();

        Hotel GetHotelFromId(int hoteltNo);
        
        bool CreateHotel(Hotel hotel);
        
        bool UpdateHotel(Hotel hotel, int hotelNo);

        bool DeleteHotel(int hotelNo);
    }
}
