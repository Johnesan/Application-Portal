using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicantValidator : AbstractValidator<AddApplicantDto>
    {
        private readonly IStringLocalizer<ApplicantValidator> _localizer;

        /// <summary>
        /// 
        /// </summary>
        public ApplicantValidator(IStringLocalizer<ApplicantValidator> localizer)
        {
            _localizer = localizer;

            RuleFor(a => a.Name).NotEmpty().MinimumLength(5).WithMessage(_localizer["NameMessage"]);
            RuleFor(a => a.FamilyName).NotEmpty().MinimumLength(5).WithMessage(_localizer["FamilyNameMessage"]);
            RuleFor(a => a.Address).NotEmpty().MinimumLength(10).WithMessage(_localizer["AddressMessage"]);
            RuleFor(a => a.CountryOfOrigin).NotEmpty().ValidCountry().WithMessage(_localizer["CountryMessage"]);
            RuleFor(a => a.EmailAddress).NotEmpty().EmailAddress().WithMessage(_localizer["EmailMessage"]);
            RuleFor(a => a.Age).InclusiveBetween(20, 60).WithMessage(_localizer["AgeMessage"]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UpdateApplicantValidator : AbstractValidator<UpdateApplicantDto>
    {
        private readonly IStringLocalizer<UpdateApplicantValidator> _localizer;

        /// <summary>
        /// 
        /// </summary>
        public UpdateApplicantValidator(IStringLocalizer<UpdateApplicantValidator> localizer)
        {
            _localizer = localizer;
            //Include(new ApplicantValidator());
        }
    }
}
