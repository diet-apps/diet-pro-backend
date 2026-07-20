using Diet.Pro.AI.Aplication.Common.Dtos;
using MediatR;
using LoginResponse = Diet.Pro.AI.Aplication.Authentication.Login.LoginResponse;

namespace Diet.Pro.AI.Aplication.Authentication.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginRequest Request { get; }

        public LoginCommand(LoginRequest request) => Request = request;
    }
}
