using Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Examples
{
    public class UpdateApplicantDtoExample : IExamplesProvider<UpdateApplicantDto>
    {
        public UpdateApplicantDto GetExamples()
        {
            return new UpdateApplicantDto
            {
                Name = "John_",
                FamilyName = "Esan_",
                EmailAddress = "jcoool40@gmail.com",
                Address = "9, Admiralty road, Lagos State",
                Age = 25,
                CountryOfOrigin = "Nigeria",
                Hired = true
            };
        }
    }
}
