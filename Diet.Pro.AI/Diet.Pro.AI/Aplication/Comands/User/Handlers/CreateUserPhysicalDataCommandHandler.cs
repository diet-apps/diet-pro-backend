using Diet.Pro.AI.Aplication.Interfaces;
using Diet.Pro.AI.Domain.Models;
using MediatR;
using OperationResult;

namespace Diet.Pro.AI.Aplication.Comands
{
    public class CreateUserPhysicalDataCommandHandler : IRequestHandler<CreateUserPhysicalDataCommand, Result<Domain.Models.User>>
    {
        private readonly IUserFirebaseService _userFirebaseService;

        public CreateUserPhysicalDataCommandHandler(IUserFirebaseService userFirebaseService) 
            => _userFirebaseService = userFirebaseService;

        public async Task<Result<Domain.Models.User>> Handle(CreateUserPhysicalDataCommand request, CancellationToken cancellationToken)
        {
            var user = new Domain.Models.User
            {
                UserId = request.UserId,
                UserPhysicalData = new UserPhysicalData
                {
                    Height = request.InputModel.Height,
                    Weight = request.InputModel.Weight,
                    PhysicalActivityLevel = request.InputModel.PhysicalActivityLevel,
                    FatPercentage = request.InputModel.FatPercentage
                }
            };

            var userCreated = await _userFirebaseService.CreateUserPhysicalDataAsync(user);
            return userCreated;
        }
    }
}
