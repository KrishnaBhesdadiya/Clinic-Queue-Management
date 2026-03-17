namespace Frontend_Exam.Models
{
    public class Clinic
    {
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public int userCount { get; set; }
        public int appointmentCount { get; set; }
        public int queueCount { get; set; }
        public DateTime createdAt { get; set; }
    }
}
