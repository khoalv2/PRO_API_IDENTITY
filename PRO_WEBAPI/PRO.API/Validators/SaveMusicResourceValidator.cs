using FluentValidation;
using PRO.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRO.API.Validators
{
    public class SaveMusicResourceValidator : AbstractValidator<SaveMusicResource>
    {
        public SaveMusicResourceValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Music Name not empty");

            RuleFor(x => x.ArtistId)
                .NotEmpty()
                .WithMessage("ArtistId not is 0");
        }
    }
}
