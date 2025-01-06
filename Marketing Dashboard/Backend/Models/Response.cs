namespace BackendAPI.Models
{
    public class Response
    {
        public int Campaign_ID { get; set; }
        public int Record_ID { get; set; }
        public bool Lead_Flag { get; set; }
        public DateTime? Lead_Timestamp { get; set; }
        public bool Funded_Flag { get; set; }
        public DateTime? Funded_Timestamp { get; set; }
    }
}