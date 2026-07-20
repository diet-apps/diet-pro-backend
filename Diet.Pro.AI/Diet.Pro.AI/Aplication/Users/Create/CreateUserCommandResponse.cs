using Diet.Pro.AI.Domain.Entities;

namespace Diet.Pro.AI.Aplication.Users.Create
{
    public sealed class CreateUserCommandResponse
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;

        public static implicit operator CreateUserCommandResponse(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new CreateUserCommandResponse
            {
                Name = user.UserData?.Name ?? string.Empty,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}
