using Diet.Pro.AI.Infra.Shared.InputModels;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Comands
{
    public sealed class CreateUserCommand : IRequest<Result<Domain.Models.User>>
    {
        public UserDataInputModel InputModel { get; set; }

        public CreateUserCommand(UserDataInputModel inputModel) => InputModel = inputModel;
    }
}
