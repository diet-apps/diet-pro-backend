using Diet.Pro.AI.Domain.Models;
using Diet.Pro.AI.Infra.Shared.InputModels;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Comands
{
    public class CreateUserPhysicalDataCommand : IRequest<Result<Domain.Models.User>>
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
