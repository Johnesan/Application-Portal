using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant
{
    public interface IApplicantService
    {
        Task FetchAllApplicants();
        Task<GetApplicantDto> GetApplicant(int Id);
        Task<GetApplicantDto> AddNewApplicant(AddApplicantDto applicant);
        Task<GetApplicantDto> UpdateApplicant(int Id, UpdateApplicantDto applicant);
        Task DeleteApplicant(int Id);
    }
}
