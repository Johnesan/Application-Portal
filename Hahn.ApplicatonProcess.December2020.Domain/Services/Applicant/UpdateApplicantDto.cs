using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant
{
    public class UpdateApplicantDto : AddApplicantDto
    {
        public bool Hired { get; set; }
    }
}
