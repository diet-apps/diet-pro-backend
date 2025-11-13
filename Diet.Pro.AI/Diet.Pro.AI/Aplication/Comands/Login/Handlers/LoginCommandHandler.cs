using Diet.Pro.AI.Aplication.Common.Dtos;
using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Aplication.Services.Cryptography;
using Diet.Pro.AI.Infra.Shared.Responses;
using Diet.Pro.AI.Shared.Exceptions;
using MediatR;

namespace Diet.Pro.AI.Aplication.Comands.Login.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseRegisteredUser>
    {
        private readonly IAuthService _authService;
        private readonly IUserFirebaseService _userFirebaseService;
        private readonly PasswordEncripter _passwordEncripter;

        public LoginCommandHandler(IAuthService authService, IUserFirebaseService userFirebaseService, PasswordEncripter passwordEncripter)
        {
            _authService = authService;
            _userFirebaseService = userFirebaseService;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<ResponseRegisteredUser> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var encriptedPassword = _passwordEncripter.Encript(request.Request.Password);

            var (_, user) = await _userFirebaseService.GetUserByEmailAndPassword(request.Request.Email, encriptedPassword);

            if (user is null)
                throw new InvalidLoginException();

            var token = _authService.GenerateJwtToken(user);

            return new ResponseRegisteredUser
            {
                Name = user.UserData?.Name ?? string.Empty,
                Tokens = new ResponseTokens
                {
                    AccessToken = token
                }
               
            };
        }
    }
}
