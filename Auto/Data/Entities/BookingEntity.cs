namespace Auto.Data.Entities
{
    public class BookingEntity
    {
        public int IdBooking { get; set; }

        public string IdUser { get; set; }
        
        public int IdSchool { get; set; }
        
        public string? Comment { get; set; }

        public UserEntity? User { get; set; }
        
        public SchoolEntity? School { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
