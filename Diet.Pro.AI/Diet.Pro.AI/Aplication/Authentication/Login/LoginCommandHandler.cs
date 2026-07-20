using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Aplication.Services.Cryptography;
using Diet.Pro.AI.Shared.Exceptions;
using MediatR;

namespace Diet.Pro.AI.Aplication.Authentication.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
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

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var encriptedPassword = _passwordEncripter.Encript(request.Request.Password);

            var (_, user) = await _userFirebaseService.GetUserByEmailAndPassword(request.Request.Email, encriptedPassword);

            if (user is null)
                throw new InvalidLoginException();

            var token = _authService.GenerateJwtToken(user);

            return new LoginResponse
            {
                Name = user.UserData?.Name ?? string.Empty,
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                AccessToken = token.AccessToken,
                ExpiresInSeconds = (int)Math.Max(0, (token.ExpiresAtUtc - DateTime.UtcNow).TotalSeconds),
                TokenType = token.TokenType,
            };
        }
    }
}
