namespace TestApplication.DTO.QueueGroup
{
    public class QueueGroupDTO
    {
        public string QueueGroupName { get; set; }
        public int Offered { get; set; }
        public int Handled { get; set; }
        public string AverageTalkTime { get; set; }
        public string AverageHandlingTime { get; set; }
        public float ServiceLevel { get; set; }
        public int SLA_Percent { get; set; }
    }
}
