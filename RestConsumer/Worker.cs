using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelModel;
using Newtonsoft.Json;

namespace RestConsumer
{
    class Worker
    {
        private const string URI = "http://localhost:2265/api/Facility";

        public void Start()
        {
            DeleteFacility(7); //Bare hvis den eksistere, så den ikke brokker sig...

            //Get all facilties
            List<Facility> facilities = GetAllFacilities();
            Console.WriteLine("Get all facilities...\n");
            foreach (var fclty in facilities)
            {
                Console.WriteLine("Facility: " + fclty.Hotel_No + " has: ");
                if (fclty.Bar == 't') Console.Write("Bar - ");
                if (fclty.PoolTable == 't') Console.Write("Pool table - ");
                if (fclty.Restaurant == 't') Console.Write("Restaurant - ");
                if (fclty.SwimmingPool == 't') Console.Write("Swimming pool - ");
                if (fclty.TableTennis == 't') Console.Write("Table tennis - ");
                Console.WriteLine("\n");
            }

            Console.WriteLine("------------------------\n");

            //Create facility
            Console.WriteLine("Creating facility with id (7)... " + (CreateFacility(new Facility(7, 't', 't', 't', 't', 't')) ? "Success!" : "Failure!"));
            
            //Get specific facility
            Console.WriteLine("Get specific facility with id (7) - newly created facility.");
            Facility facility = GetOneFacility(7);
            Console.WriteLine("Facility: " + facility.Hotel_No + " has: ");
            if (facility.Bar == 't') Console.Write("Bar - ");
            if (facility.PoolTable == 't') Console.Write("Pool table - ");
            if (facility.Restaurant == 't') Console.Write("Restaurant - ");
            if (facility.SwimmingPool == 't') Console.Write("Swimming pool - ");
            if (facility.TableTennis == 't') Console.Write("Table tennis - ");

            Console.WriteLine("\n------------------------\n");
            
            //Update Facility
            Console.WriteLine("Updating facility with id (7)..." + (UpdateFacility(7, new Facility(7, 'f', 'f', 'f', 'f', 'f')) ? "Success!" : "Failure!"));

            Console.WriteLine();

            Console.WriteLine("Get specific facility with id (7) - newly updated facility");
            facility = GetOneFacility(7);
            Console.WriteLine("Facility: " + facility.Hotel_No + " has: ");
            if (facility.Bar == 't') Console.Write("Bar - ");
            if (facility.PoolTable == 't') Console.Write("Pool table - ");
            if (facility.Restaurant == 't') Console.Write("Restaurant - ");
            if (facility.SwimmingPool == 't') Console.Write("Swimming pool - ");
            if (facility.TableTennis == 't') Console.Write("Table tennis - ");

            Console.WriteLine("\n------------------------\n");
            
            //Delete Facility
            Console.WriteLine("Deleting facility with id (7)..." + (DeleteFacility(7) ? "Success!" : "Failure!"));

            Console.WriteLine("Get all facilities...\n");
            foreach (var fclty in facilities)
            {
                Console.WriteLine("Facility: " + fclty.Hotel_No + " has: ");
                if (fclty.Bar == 't') Console.Write("Bar - ");
                if (fclty.PoolTable == 't') Console.Write("Pool table - ");
                if (fclty.Restaurant == 't') Console.Write("Restaurant - ");
                if (fclty.SwimmingPool == 't') Console.Write("Swimming pool - ");
                if (fclty.TableTennis == 't') Console.Write("Table tennis - ");
                Console.WriteLine("\n");
            }

            //Test for failure with delete
            Console.WriteLine("Test for falure");
            Console.WriteLine("Deleting facility with id (9999)..." + (DeleteFacility(9999) ? "Success!" : "Failure!"));
            Console.WriteLine("Updating facility with id (9999)..." + (UpdateFacility(9999, new Facility()) ? "Success!" : "Failure!"));
            Console.WriteLine("Creating facility with id (1)..." + (CreateFacility(new Facility(1,'f', 'f', 'f', 'f', 'f')) ? "Success!" : "Failure!"));

        }

        private List<Facility> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI);
                String jsonStr = resTask.Result;

                facilities = JsonConvert.DeserializeObject<List<Facility>>(jsonStr);
            }

            return facilities;
        }

        private Facility GetOneFacility(int id)
        {
            Facility facility = new Facility();

            using (HttpClient client = new HttpClient())
            {
                Task<string> resTask = client.GetStringAsync(URI + "/" + id);
                String jsonStr = resTask.Result;

                facility = JsonConvert.DeserializeObject<Facility>(jsonStr);
            }

            return facility;
        }
        private bool DeleteFacility(int id)
        {
            bool output = true;

            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

                HttpResponseMessage resp = deleteAsync.Result;
                //if (!resp.IsSuccessStatusCode) output = false;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonStr = resp.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<bool>(jsonStr);
                }
                else
                {
                    output = false;
                }

            }

            return output;
        }

        private bool CreateFacility(Facility facility)
        {
            bool output = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

                Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

                HttpResponseMessage resp = postAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<bool>(jsonResStr);
                }
                else
                {
                    output = false;
                }
            }

            return output;
        }

        private bool UpdateFacility(int id, Facility facility)
        {
            bool output = true;

            using (HttpClient client = new HttpClient())
            {
                String jsonStr = JsonConvert.SerializeObject(facility);
                StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

                Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

                HttpResponseMessage resp = putAsync.Result;
                if (resp.IsSuccessStatusCode)
                {
                    String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                    output = JsonConvert.DeserializeObject<bool>(jsonResStr);
                }
                else
                {
                    output = false;
                }
            }

            return output;
        }
    }
}
