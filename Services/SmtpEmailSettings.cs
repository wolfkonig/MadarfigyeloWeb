namespace MadarfigyeloWeb.Services
{
    public class SmtpEmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string FromEmail { get; set; } = string.Empty;
        public string? FromName { get; set; }
    }
}
