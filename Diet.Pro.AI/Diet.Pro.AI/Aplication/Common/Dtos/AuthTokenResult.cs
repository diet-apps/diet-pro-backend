namespace Diet.Pro.AI.Aplication.Common.Dtos
{
    public class AuthTokenResult
    {
        public string AccessToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public DateTime ExpiresAtUtc { get; set; }
    }
}