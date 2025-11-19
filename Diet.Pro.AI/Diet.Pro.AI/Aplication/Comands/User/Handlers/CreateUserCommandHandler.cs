using Diet.Pro.AI.Aplication.Comands.User.Validators;
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

        public CreateUserCommandHandler(IUserFirebaseService userFirebaseService, PasswordEncripter passwordEncripter)
        {
            _userFirebaseService = userFirebaseService;
            _passwordEncripter = passwordEncripter;
        }

        public async Task<Result<Domain.Models.User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Validate(request);

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
            return userCreated!;
        }
        private void Validate(CreateUserCommand request)
        {
            var validator = new CreateUserCommandValidator();

            var result = validator.Validate(request);

            //criar validação para tratr email existente!
            //var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            //if (emailExist)
            //    result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTRED));

            if (result.IsValid is false)
            {
                var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
