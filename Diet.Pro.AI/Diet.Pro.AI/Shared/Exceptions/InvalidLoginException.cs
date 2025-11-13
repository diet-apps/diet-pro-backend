namespace Diet.Pro.AI.Shared.Exceptions
{
    public class InvalidLoginException : DietProAiException
    {
        public InvalidLoginException() : base("E-mail e/ou senha inválidos.")
        {
        }
    }
}
