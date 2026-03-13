using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebsiteBanHang_NguyenNhatTruong.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask;
        }
    }
}