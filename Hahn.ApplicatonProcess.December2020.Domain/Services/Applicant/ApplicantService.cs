using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant
{
    public class ApplicantService : IApplicantService
    {
        private readonly IBaseRepository<Data.Entities.Applicant, int> _repo;
        private readonly IMapper _mapper;

        public ApplicantService(IBaseRepository<Data.Entities.Applicant, int> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<GetApplicantDto> AddNewApplicant(AddApplicantDto applicantDto)
        {
            var addedEntity = await _repo.Insert(_mapper.Map<Data.Entities.Applicant>(applicantDto));
            return _mapper.Map<GetApplicantDto>(addedEntity);
        }

        public async Task DeleteApplicant(int Id)
        {
            var applicant = await _repo.Get(Id);
            if (applicant == null)
                throw new KeyNotFoundException("Applicant not found.");

            await _repo.Delete(applicant);
        }

        public async Task<GetApplicantDto> UpdateApplicant(int Id, UpdateApplicantDto applicant)
        {
            var existingApplicant = await _repo.Get(Id);
            if (existingApplicant == null)
                throw new KeyNotFoundException("Applicant not found.");

             _mapper.Map(applicant, existingApplicant);
            await _repo.Update(existingApplicant);
            return _mapper.Map<GetApplicantDto>(existingApplicant);
        }

        public async Task FetchAllApplicants()
        {
            throw new NotImplementedException();
        }

        public async Task<GetApplicantDto> GetApplicant(int Id)
        {
            var applicant = await _repo.Get(Id);
            if (applicant == null)
                throw new KeyNotFoundException($"Applicant not found.");

            return _mapper.Map<GetApplicantDto>(applicant);
        }
    }
}
