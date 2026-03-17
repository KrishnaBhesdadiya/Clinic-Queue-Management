namespace Frontend_Exam.Models
{
    public class LoginResponse
    {
        public string token { get; set; }
        public LoginData user { get; set; }
    }

    public class LoginData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public int clinicId { get; set; }
        public string clinicName { get; set; }
        public string clinicCode { get; set; }
    }
    public class HealthResponse
    {
        public bool ok { get; set; }
    }
}
