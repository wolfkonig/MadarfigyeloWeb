namespace MadarfigyeloWeb.Services
{
    public interface IAccountEmailSender
    {
        Task SendPasswordResetLinkAsync(string email, string resetLink);
    }
}
