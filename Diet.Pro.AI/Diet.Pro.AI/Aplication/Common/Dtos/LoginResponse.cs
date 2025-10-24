namespace Diet.Pro.AI.Aplication.Common.Dtos
{
    public class LoginResponse
    {
        public string Token { get; init; } = string.Empty;
        public string UserId { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
    }
}
