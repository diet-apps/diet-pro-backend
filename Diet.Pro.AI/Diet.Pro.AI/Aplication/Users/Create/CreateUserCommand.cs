using Diet.Pro.AI.Infra.Shared.InputModels;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Users.Create
{
    public sealed class CreateUserCommand : IRequest<Result<CreateUserCommandResponse>>
    {
        public UserDataInputModel InputModel { get; set; }

        public CreateUserCommand(UserDataInputModel inputModel) => InputModel = inputModel;
    }
}
