using FluentValidation;

namespace Diet.Pro.AI.Aplication.Comands.User.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.InputModel.Name).NotEmpty().WithMessage("O nome não pode estar vazio.");
            RuleFor(user => user.InputModel.DateOfBirth).LessThan(DateTime.Now).WithMessage("A data de nascimento deve ser uma data válida.");
            RuleFor(user => user.InputModel.Sex).NotEmpty().WithMessage("O sexo não pode estar vazio.");
            RuleFor(user => user.InputModel.Email).NotEmpty().WithMessage("O e-mail não pode estar vazio.");
            RuleFor(user => user.InputModel.PasswordHash.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve ter mais de 6 caracteres.");
            When(user => string.IsNullOrEmpty(user.InputModel.Email) is false, () =>
            {
                RuleFor(user => user.InputModel.Email).EmailAddress().WithMessage("O endereço de e-mail é inválido.");
            });
        }
    }
}
