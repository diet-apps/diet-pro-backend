using Diet.Pro.AI.Domain.Entities;
using Diet.Pro.AI.Infra.Shared.InputModels;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Users.Create
{
    public class CreateUserPhysicalDataCommand : IRequest<Result<User>>
    {
        public string UserId { get; set; }
        public UserPhysicalDataInputModel InputModel { get; set; }

        public CreateUserPhysicalDataCommand(string userId, UserPhysicalDataInputModel inputModel)
        {
            UserId = userId;
            InputModel = inputModel;
        }
    }
}
