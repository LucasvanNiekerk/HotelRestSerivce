using System;
using HotelModel;
using RestConsumer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        private const int _testFacilityNo = 7;
        [TestMethod]
        public void TestWorker()
        {
            //Create instance of worker class
            Worker worker = new Worker();
            //Delete test facility to be sure everything works
            worker.DeleteFacility(_testFacilityNo);

            //Fetch list of facilities from database
            List<Facility> facilities = worker.GetAllFacilities();

            //Keep number of facilities
            int orgNumOffacilities = facilities.Count;

            //Create new facility in databse
            Facility newFacility = new Facility(_testFacilityNo,'f', 'f', 'f', 'f', 'f');
            worker.CreateFacility(newFacility);

            //Get new lost of facilities
            List<Facility> facilityPlusOne = worker.GetAllFacilities();

            //Get count of facilities
            int newNumOfFacilities = facilityPlusOne.Count;

            //Test on number of guests
            Assert.AreEqual(orgNumOffacilities + 1, newNumOfFacilities, 0 , "The number of facilities are not equal");

            //Fetch created facility
            Facility fetchedNewFacility = worker.GetOneFacility(_testFacilityNo);

            //Assert that the fetched facility is the same as the facility we created
            Assert.AreEqual(newFacility.Hotel_No, fetchedNewFacility.Hotel_No);
            Assert.AreEqual(newFacility.Bar, fetchedNewFacility.Bar);
            Assert.AreEqual(newFacility.Pool_Table, fetchedNewFacility.Pool_Table);
            Assert.AreEqual(newFacility.Restaurant, fetchedNewFacility.Restaurant);
            Assert.AreEqual(newFacility.Swimming_Pool, fetchedNewFacility.Swimming_Pool);
            Assert.AreEqual(newFacility.Table_Tennis, fetchedNewFacility.Table_Tennis);
            
            //Update the facility we created
            Facility updatedFacility = new Facility(_testFacilityNo, 't', 't', 't', 't', 't');
            worker.UpdateFacility(7, updatedFacility);

            //Fetch the updated facility
            Facility fetchedUpdatedFacility = worker.GetOneFacility(_testFacilityNo);

            //Assert that the fetched updated facility is the same as the facility we updated
            Assert.AreEqual(updatedFacility.Hotel_No, fetchedUpdatedFacility.Hotel_No);
            Assert.AreEqual(updatedFacility.Bar, fetchedUpdatedFacility.Bar);
            Assert.AreEqual(updatedFacility.Pool_Table, fetchedUpdatedFacility.Pool_Table);
            Assert.AreEqual(updatedFacility.Restaurant, fetchedUpdatedFacility.Restaurant);
            Assert.AreEqual(updatedFacility.Swimming_Pool, fetchedUpdatedFacility.Swimming_Pool);
            Assert.AreEqual(updatedFacility.Table_Tennis, fetchedUpdatedFacility.Table_Tennis);

            //We delete the updated facility
            worker.DeleteFacility(_testFacilityNo);

            //We fetch the deleted facility
            Facility deletedFacility = worker.GetOneFacility(_testFacilityNo);

            //Assert that the fetched deleted facility is not the same as the updated facility as it is "empty"
            Assert.AreNotEqual(deletedFacility.Hotel_No, fetchedUpdatedFacility.Hotel_No);
            Assert.AreNotEqual(deletedFacility.Bar, fetchedUpdatedFacility.Bar);
            Assert.AreNotEqual(deletedFacility.Pool_Table, fetchedUpdatedFacility.Pool_Table);
            Assert.AreNotEqual(deletedFacility.Restaurant, fetchedUpdatedFacility.Restaurant);
            Assert.AreNotEqual(deletedFacility.Swimming_Pool, fetchedUpdatedFacility.Swimming_Pool);
            Assert.AreNotEqual(deletedFacility.Table_Tennis, fetchedUpdatedFacility.Table_Tennis);

        }
    }
}
