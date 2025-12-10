using MediatR;

namespace Diet.Pro.AI.Aplication.Events.User.Register
{
    public record UserRegisteredEvent(string Name, string Email) : INotification;
}
