using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.CreateNote
{
    //Валидация с помощью FluentValidationю
    //По сути просто клас, в конструктре которого задаем правила для нужного класса.
    public class CreatenoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreatenoteCommandValidator()
        {
            RuleFor(createNoteCommand => createNoteCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createNoteCommand => createNoteCommand.UserId).NotEqual(Guid.Empty);
        }

    }
}
