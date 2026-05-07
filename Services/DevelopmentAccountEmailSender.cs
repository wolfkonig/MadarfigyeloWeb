using Microsoft.Extensions.Logging;

namespace MadarfigyeloWeb.Services
{
    public class DevelopmentAccountEmailSender : IAccountEmailSender
    {
        private readonly ILogger<DevelopmentAccountEmailSender> _logger;

        public DevelopmentAccountEmailSender(ILogger<DevelopmentAccountEmailSender> logger)
        {
            _logger = logger;
        }

        public Task SendPasswordResetLinkAsync(string email, string resetLink)
        {
            _logger.LogInformation("Password reset requested for {Email}. Reset link: {ResetLink}", email, resetLink);
            return Task.CompletedTask;
        }
    }
}
