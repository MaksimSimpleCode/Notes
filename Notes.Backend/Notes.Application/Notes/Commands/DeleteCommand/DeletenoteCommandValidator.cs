using FluentValidation;
using System;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeletenoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeletenoteCommandValidator()
        {
            RuleFor(updateNoteCommand => updateNoteCommand.Id).NotEqual(Guid.Empty);
            RuleFor(updateNoteCommand => updateNoteCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
