namespace TestApplication.Models
{
    public class Monitor
    {
        public int Id { get; set; }
        public int TalkTime { get; set; }
        public int AfterCallWorkTime { get; set; }
        public int Handled { get; set; }
        public int Offered { get; set; }
        public int HandledWithinSL { get; set; }
        public int QueueGroupID { get; set; }

        // Navigation property
        public virtual QueueGroup QueueGroup { get; set; }
    }
}
