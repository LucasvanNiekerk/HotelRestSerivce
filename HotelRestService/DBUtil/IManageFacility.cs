using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageFacility
    {
        List<Facility> GetAllFacilities();


        Facility GetFacilityFromId(int Hotel_No);


        bool CreateFacility(Facility facility);


        bool UpdateFacility(Facility facility, int Hotel_No);


        bool DeleteFacility(int Hotel_No);
    }
}
