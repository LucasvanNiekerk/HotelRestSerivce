using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageGuest
    {
        List<Guest> GetAllGuest();
        
        Guest GetGuestFromId(int guestNr);

        bool CreateGuest(Guest guest);
        
        bool UpdateGuest(Guest guest, int guestNr);
        
        bool DeleteGuest(int guestNr);
    }
}
