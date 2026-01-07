using Diet.Pro.AI.Aplication.Comands.User.Validators;
using Diet.Pro.AI.Aplication.Events.User.Register;
using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Aplication.Services.Cryptography;
using Diet.Pro.AI.Domain.Models;
using Diet.Pro.AI.Shared.Exceptions;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Comands
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Domain.Models.User>>
    {
        private readonly IUserFirebaseService _userFirebaseService;
        private readonly PasswordEncripter _passwordEncripter;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IUserFirebaseService userFirebaseService, PasswordEncripter passwordEncripter, IMediator mediator)
        {
            _userFirebaseService = userFirebaseService;
            _passwordEncripter = passwordEncripter;
            _mediator = mediator;
        }

        public async Task<Result<Domain.Models.User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Validate(request);

            var userId = Guid.NewGuid().ToString();

            var user = new Domain.Models.User
            {
                UserId = userId,
                Email = request.InputModel.Email,
                PasswordHash = _passwordEncripter.Encript(request.InputModel.PasswordHash),
                UserData = new UserData
                {
                    Name = request.InputModel.Name,
                    DateOfBirth = request.InputModel.DateOfBirth.ToUniversalTime(),
                    Sex = request.InputModel.Sex
                }
            };

            var userCreated = await _userFirebaseService.CreateUserDataAsync(user);

            var (name, email) = GetUserInfoForSendEmail(userCreated.Value!);

            await _mediator.Publish(new UserRegisteredEvent(name, email), cancellationToken);

            return userCreated!;
        }
        private async Task Validate(CreateUserCommand request)
        {
            var validator = new CreateUserCommandValidator();

            var result = validator.Validate(request);

            var emailExist = await _userFirebaseService.ExistActiveUserWithEmail(request.InputModel.Email);
            if (emailExist)
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, "Email já cadastrado."));

            if (result.IsValid is false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }

        private static (string name, string email) GetUserInfoForSendEmail(Domain.Models.User user) => (user.UserData!.Name, user.Email);
    }
}
