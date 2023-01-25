using CORE.Entities;
using FluentValidation;

namespace REPOSITORY.Validators;

public class MediaValidator : AbstractValidator<Media>
{
    public MediaValidator()
    {
        RuleFor(p => p.MediaTypeId)
            .NotEmpty().WithMessage("გთხოვთ მიუთითოთ მედიის ტიპი!");

        RuleFor(p => p.MediaTitle)
            .NotEmpty().WithMessage("გთხოვთ მიუთითოთ ფილმის დასახელება!")
            .Length(5, 100).WithMessage("მინიმალური სიმბოლოთა რაოდენობა 5 სიმბოლო, მაქსიმალური 100 სიმბოლო!");

        RuleFor(p => p.PictureUrl)
            .NotEmpty().WithMessage("გთხოვთ მიუთითოთ ფილმის პოსტერის მისამართი!")
            .Length(5, 100).WithMessage("მინიმალური სიმბოლოთა რაოდენობა 5 სიმბოლო, მაქსიმალური 100 სიმბოლო!");
    }
}