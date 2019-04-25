namespace HotelModel
{
    public class Facility
    {
        public Facility()
        {
            
        }

        public Facility(int hotel_No, char swimming_Pool, char bar, char table_Tennis, char pool_Table, char restaurant)
        {
            Hotel_No = hotel_No;
            Swimming_Pool = swimming_Pool;
            Bar = bar;
            Table_Tennis = table_Tennis;
            Pool_Table = pool_Table;
            Restaurant = restaurant;
        }

        public int Hotel_No { get; set; } //PK
        public char Swimming_Pool { get; set; }
        public char Bar { get; set; }
        public char Table_Tennis { get; set; }
        public char Pool_Table { get; set; }
        public char Restaurant { get; set; }
    }
}
