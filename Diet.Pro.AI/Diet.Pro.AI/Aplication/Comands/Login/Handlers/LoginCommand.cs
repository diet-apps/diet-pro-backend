using Diet.Pro.AI.Aplication.Common.Dtos;
using Diet.Pro.AI.Infra.Shared.Responses;
using MediatR;

namespace Diet.Pro.AI.Aplication.Comands.Login.Handlers
{
    public class LoginCommand : IRequest<ResponseRegisteredUser>
    {
        public LoginRequest Request { get; }

        public LoginCommand(LoginRequest request) => Request = request;
    }
}
