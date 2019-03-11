using System.Collections.Generic;
using System.Web.Http;
using HotelModel;
using HotelRestService.DBUtil;

namespace HotelRestService.Controllers
{
    public class FacilityController : ApiController
    {
        ManageFacility _mngFacility = new ManageFacility();

        public IEnumerable<Facility> Get()
        {
            return _mngFacility.GetAllFacilities();
        }

        public Facility Get(int id)
        {
            return _mngFacility.GetFacilityFromId(id);
        }
        public bool Post([FromBody]Facility value)
        {
            return _mngFacility.CreateFacility(value);
        }
        public bool Put(int id, [FromBody]Facility value)
        {
            return _mngFacility.UpdateFacility(value, id);
        }
        public bool Delete(int id)
        {
            return _mngFacility.DeleteFacility(id);
        }
    }
}
