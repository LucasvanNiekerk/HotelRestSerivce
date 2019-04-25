using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class FacilityController : ApiController
    {
        ManageFacilityTest _mngFacility = new ManageFacilityTest();

        public IEnumerable<Facility> Get()
        {
            return _mngFacility.GetAllOfType(new Facility(), "Facility");
        }

        public Facility Get(int id)
        {
            return _mngFacility.GetOneFromId(new Facility(), "Facility", id);
        }
        public bool Post([FromBody]Facility value)
        {
            return _mngFacility.Create(value, "Facility");
        }
        public bool Put(int id, [FromBody]Facility value)
        {
            return _mngFacility.Update(value, "Facility", "Hotel_No", id);
        }
        public bool Delete(int id)
        {
            return _mngFacility.Delete("Facility", "Hotel_No", id);
        }
    }
}
