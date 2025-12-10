using MediatR;

namespace Diet.Pro.AI.Aplication.Events.User.Register
{
    public class UserRegisteredEventHandler : INotificationHandler<UserRegisteredEvent>
    {
        public Task Handle(UserRegisteredEvent notification, CancellationToken cancellationToken)
        {
            //enviar email de boas vindas utilizando um serviço de email
            Console.WriteLine("Enviar email");
            return Task.CompletedTask;
        }
    }
}
