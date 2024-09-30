namespace TestApplication.Models
{
    public class QueueGroup
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int SLA_Percent { get; set; }
        public int SLA_Time { get; set; }
    }
}
