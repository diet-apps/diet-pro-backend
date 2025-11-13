namespace Diet.Pro.AI.Shared.Exceptions
{
    public class ErrorOnValidationException : DietProAiException
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages) : base(string.Empty) => ErrorMessages = errorMessages;
    }
}
