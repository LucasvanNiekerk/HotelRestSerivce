namespace HotelModel
{
    public class Facility
    {
        public Facility()
        {
            
        }

        public Facility(int hotel_No, char bar, char tableTennis, char poolTable, char restaurant, char swimmingPool)
        {
            Hotel_No = hotel_No;
            Bar = bar;
            TableTennis = tableTennis;
            PoolTable = poolTable;
            Restaurant = restaurant;
            SwimmingPool = swimmingPool;
        }

        public int Hotel_No { get; set; }
        public char Bar { get; set; }
        public char TableTennis { get; set; }
        public char PoolTable { get; set; }
        public char Restaurant { get; set; }
        public char SwimmingPool { get; set; }
    }
}
