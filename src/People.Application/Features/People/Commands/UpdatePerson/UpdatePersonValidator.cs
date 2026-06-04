using FluentValidation;

namespace People.Application.Features.People.Commands.UpdatePerson;

public class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
{
    public UpdatePersonValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("O ID do colaborador deve ser informado e válido.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(150).WithMessage("O nome não pode exceder 150 caracteres.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("O cargo é obrigatório.")
            .MaximumLength(100).WithMessage("O cargo não pode exceder 100 caracteres.");

        RuleFor(x => x.Department)
            .NotEmpty().WithMessage("O setor é obrigatório.")
            .MaximumLength(100).WithMessage("O setor não pode exceder 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado não é válido.")
            .MaximumLength(150).WithMessage("O e-mail não pode exceder 150 caracteres.");

        RuleFor(x => x.Status)
            .MaximumLength(20).WithMessage("O status não pode exceder 20 caracteres.");
    }
}
