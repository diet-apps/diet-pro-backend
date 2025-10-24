using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Domain.Models;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Comands
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<User>>
    {
        private readonly IUserFirebaseService _userFirebaseService;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserFirebaseService userFirebaseService, IAuthService authService)
        {
            _userFirebaseService = userFirebaseService;
            _authService = authService;
        }

        public async Task<Result<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Guid.NewGuid().ToString();

                var hashedPassword = _authService.HashPassword(request.InputModel.PasswordHash);

                var user = new User
                {
                    UserId = userId,
                    Email = request.InputModel.Email,
                    PasswordHash = hashedPassword,
                    UserData = new UserData
                    {
                        Name = request.InputModel.Name,
                        DateOfBirth = request.InputModel.DateOfBirth.ToUniversalTime(),
                        Sex = request.InputModel.Sex
                    }
                };

                var userCreated = await _userFirebaseService.CreateUserDataAsync(user);
                return userCreated;
            }
            catch (Exception)
            {
                //Melhorar tratamento!
                throw;
            }
            
        }
    }
}
