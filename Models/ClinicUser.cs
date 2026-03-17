namespace Frontend_Exam.Models
{
    public class ClinicUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public string phone { get; set; }
        public DateTime createdAt { get; set; }
        public string password { get; set; }
    }
}
