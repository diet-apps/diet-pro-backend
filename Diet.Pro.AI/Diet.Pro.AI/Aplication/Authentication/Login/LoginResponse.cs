namespace Diet.Pro.AI.Aplication.Authentication.Login
{
    public sealed class LoginResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresInSeconds { get; set; } 
        public string? RefreshToken { get; set; }
    }
}