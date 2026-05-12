using Diet.Pro.AI.Aplication.Common.Dtos;

namespace Diet.Pro.AI.Aplication.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
}
