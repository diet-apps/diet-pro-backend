using Diet.Pro.AI.Aplication.Common.Dtos;
using Diet.Pro.AI.Aplication.Interfaces;
using MediatR;

namespace Diet.Pro.AI.Aplication.Comands.Login.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IAuthService _authService;
        private readonly IUserFirebaseService _userFirebaseService;

        public LoginCommandHandler(IAuthService authService, IUserFirebaseService userFirebaseService)
        {
            _authService = authService;
            _userFirebaseService = userFirebaseService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var (_, user) = await _userFirebaseService.GetUserByEmailAsync(request.Request.Email);

            if (user is null || !_authService.VerifyPassword(request.Request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Credenciais inválidas.");

            var token = _authService.GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                UserId = user.UserId,
                Email = user.Email,
                Name = user.UserData?.Name ?? string.Empty
            };
        }
    }
}
