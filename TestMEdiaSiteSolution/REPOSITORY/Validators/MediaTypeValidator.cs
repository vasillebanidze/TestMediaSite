using CORE.Entities;
using FluentValidation;

namespace REPOSITORY.Validators;

public class MediaTypeValidator : AbstractValidator<MediaType>
{
    public MediaTypeValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(p => p.MediaTypeTitle)
            .NotEmpty().WithMessage("გთხოვთ მიუთითოთ ფილმის ტიპის დასახელება!")
            .Length(5, 100).WithMessage("მინიმალური სიმბოლოთა რაოდენობა 5 სიმბოლო, მაქსიმალური 100 სიმბოლო!");
    }
}