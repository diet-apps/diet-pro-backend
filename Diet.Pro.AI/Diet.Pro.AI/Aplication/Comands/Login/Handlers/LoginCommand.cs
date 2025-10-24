using Diet.Pro.AI.Aplication.Common.Dtos;
using MediatR;

namespace Diet.Pro.AI.Aplication.Comands.Login.Handlers
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest Request { get; }

        public LoginCommand(LoginRequest request) => Request = request;
    }
}
