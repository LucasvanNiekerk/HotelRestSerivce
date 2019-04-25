using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotelModel;
using Newtonsoft.Json;
using System.Threading;

namespace RestConsumer
{
    public class Worker
    {
        private const string URI = "http://localhost:2265/api/Facility";
        private HttpClient client = Utils.Client();

        //public void Start()
        //{
        //    DeleteFacility(7); //Det er hvis facility 7 eksistere, så brokker den ikke...

        //    //Get all facilties
        //    List<Facility> facilities = GetAllFacilities();
        //    Console.WriteLine("Get all facilities...\n");
        //    foreach (var fclty in facilities)
        //    {
        //        Console.WriteLine("Facility: " + fclty.Hotel_No + " has: ");
        //        if (fclty.Bar == 't') Console.Write("Bar - ");
        //        if (fclty.Pool_Table == 't') Console.Write("Pool table - ");
        //        if (fclty.Restaurant == 't') Console.Write("Restaurant - ");
        //        if (fclty.Swimming_Pool == 't') Console.Write("Swimming pool - ");
        //        if (fclty.Table_Tennis == 't') Console.Write("Table tennis - ");
        //        Console.WriteLine("\n");
        //    }

        //    Console.WriteLine("------------------------\n");

        //    //Create facility
        //    Console.WriteLine("Creating facility with id (7)... " + (CreateFacility(new Facility(7, 't', 't', 't', 't', 't')) ? "Success!" : "Failure!"));

        //    //Get specific facility
        //    Console.WriteLine("Get specific facility with id (7) - newly created facility.");
        //    Facility facility = GetOneFacility(7);
        //    Console.WriteLine("Facility: " + facility.Hotel_No + " has: ");
        //    if (facility.Bar == 't') Console.Write("Bar - ");
        //    if (facility.Pool_Table == 't') Console.Write("Pool table - ");
        //    if (facility.Restaurant == 't') Console.Write("Restaurant - ");
        //    if (facility.Swimming_Pool == 't') Console.Write("Swimming pool - ");
        //    if (facility.Table_Tennis == 't') Console.Write("Table tennis - ");

        //    Console.WriteLine("\n------------------------\n");

        //    //Update Facility
        //    Console.WriteLine("Updating facility with id (7)..." + (UpdateFacility(7, new Facility(7, 'f', 'f', 'f', 'f', 'f')) ? "Success!" : "Failure!"));

        //    Console.WriteLine();

        //    Console.WriteLine("Get specific facility with id (7) - newly updated facility");
        //    facility = GetOneFacility(7);
        //    Console.WriteLine("Facility: " + facility.Hotel_No + " has: ");
        //    if (facility.Bar == 't') Console.Write("Bar - ");
        //    if (facility.Pool_Table == 't') Console.Write("Pool table - ");
        //    if (facility.Restaurant == 't') Console.Write("Restaurant - ");
        //    if (facility.Swimming_Pool == 't') Console.Write("Swimming pool - ");
        //    if (facility.Table_Tennis == 't') Console.Write("Table tennis - ");

        //    Console.WriteLine("\n------------------------\n");

        //    //Delete Facility
        //    Console.WriteLine("Deleting facility with id (7)..." + (DeleteFacility(7) ? "Success!" : "Failure!"));

        //    Console.WriteLine("Get all facilities...\n");
        //    foreach (var fclty in facilities)
        //    {
        //        Console.WriteLine("Facility: " + fclty.Hotel_No + " has: ");
        //        if (fclty.Bar == 't') Console.Write("Bar - ");
        //        if (fclty.Pool_Table == 't') Console.Write("Pool table - ");
        //        if (fclty.Restaurant == 't') Console.Write("Restaurant - ");
        //        if (fclty.Swimming_Pool == 't') Console.Write("Swimming pool - ");
        //        if (fclty.Table_Tennis == 't') Console.Write("Table tennis - ");
        //        Console.WriteLine("\n");
        //    }

        //    //Test for failure with delete
        //    Console.WriteLine("Test for failure");
        //    Console.WriteLine("Deleting facility with id (9999)..." + (DeleteFacility(9999) ? "Success!" : "Failure!"));
        //    Console.WriteLine("Updating facility with id (9999)..." + (UpdateFacility(9999, new Facility()) ? "Success!" : "Failure!"));
        //    Console.WriteLine("Creating facility with id (1)..." + (CreateFacility(new Facility(1, 'f', 'f', 'f', 'f', 'f')) ? "Success!" : "Failure!"));
        //}

        public void Start()
        {
            var t = GetOneFacility(1);
            Console.WriteLine(t.Hotel_No);

            DeleteFacility(1);
            CreateFacility(new Facility(1, 'f', 'f', 'f', 'f', 'f'));
            var tt = GetOneFacility(1);
            Console.WriteLine(tt.Hotel_No);
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //var test = GetAllFacilities();
            //Console.WriteLine(sw.ElapsedMilliseconds);

            //for (int i = 0; i < 25; i++)
            //{
            //    sw.Restart();
            //    var test2 = GetAllFacilities();
            //    Console.WriteLine(sw.ElapsedMilliseconds);
            //}

            //foreach (var fclty in test)
            //{
            //    Console.WriteLine("Facility: " + fclty.Hotel_No + " has: ");
            //    if (fclty.Bar == 't') Console.Write("Bar - ");
            //    if (fclty.Pool_Table == 't') Console.Write("Pool table - ");
            //    if (fclty.Restaurant == 't') Console.Write("Restaurant - ");
            //    if (fclty.Swimming_Pool == 't') Console.Write("Swimming pool - ");
            //    if (fclty.Table_Tennis == 't') Console.Write("Table tennis - ");
            //    Console.WriteLine("\n");
            //}
        }
        public List<Facility> GetAllFacilities()
        {
            Task<string> resTask = client.GetStringAsync(URI);

            return JsonConvert.DeserializeObject<List<Facility>>(resTask.Result);
        }

        public Facility GetOneFacility(int id)
        {
            Task<string> resTask = client.GetStringAsync(URI + "/" + id);

            return JsonConvert.DeserializeObject<Facility>(resTask.Result);
        }
        public bool DeleteFacility(int id)
        {
            Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(URI + "/" + id);

            HttpResponseMessage resp = deleteAsync.Result;
            if (resp.IsSuccessStatusCode)
            {
                String jsonStr = resp.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<bool>(jsonStr);
            }
            return false;
        }

        public bool CreateFacility(Facility facility)
        {
            String jsonStr = JsonConvert.SerializeObject(facility);
            StringContent content = new StringContent(jsonStr, Encoding.ASCII, "application/json");

            Task<HttpResponseMessage> postAsync = client.PostAsync(URI, content);

            HttpResponseMessage resp = postAsync.Result;
            if (resp.IsSuccessStatusCode)
            {
                String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<bool>(jsonResStr);
            }
            return false;
        }

        public bool UpdateFacility(int id, Facility facility)
        {
            String jsonStr = JsonConvert.SerializeObject(facility);
            StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            Task<HttpResponseMessage> putAsync = client.PutAsync(URI + "/" + id, content);

            HttpResponseMessage resp = putAsync.Result;
            if (resp.IsSuccessStatusCode)
            {
                String jsonResStr = resp.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<bool>(jsonResStr);
            }
            return false;
        }
    }
}