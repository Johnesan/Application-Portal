using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant
{
    public class GetApplicantDto : AddApplicantDto
    {
        public int Id { get; set; }
        public bool Hired { get; set; }

    }
}
