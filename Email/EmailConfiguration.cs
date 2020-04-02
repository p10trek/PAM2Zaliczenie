namespace PAM2Zaliczenie.Email
{
    public interface IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SSL { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
    }

    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SSL { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
    }
}