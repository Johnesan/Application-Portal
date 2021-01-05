using AutoMapper;
using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddApplicantDto, Applicant>();
            CreateMap<GetApplicantDto, Applicant>();
            CreateMap<UpdateApplicantDto, Applicant>();
            CreateMap<Applicant, AddApplicantDto>();
            CreateMap<Applicant, GetApplicantDto>();
            CreateMap<Applicant, UpdateApplicantDto>();
        }
    }
}
