using Hahn.ApplicatonProcess.December2020.Domain.Services.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.Examples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("applicant")]
    [ApiController]
    public class ApplicantController : Controller
    {
        private readonly IStringLocalizer<ApplicantController> _localizer;
        private readonly IApplicantService _applicantService;

        public ApplicantController(IStringLocalizer<ApplicantController> localizer, IApplicantService applicantService)
        {
            _localizer = localizer;
            _applicantService = applicantService;
        }

        // GET: ApplicantController/Details/5
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var applicant = await _applicantService.GetApplicant(id);
            if (applicant == null)
                throw new Exception(_localizer["DefaultExceptionMessage"]);

            return Ok(applicant);
        }

        // POST: ApplicantController/Create
        [HttpPost("Create")]
        [SwaggerRequestExample(typeof(AddApplicantDto), typeof(AddApplicantDtoExample))]
        public async Task<IActionResult> Create([FromBody] AddApplicantDto applicant)
        {
            var applicantToReturn = await _applicantService.AddNewApplicant(applicant);
            if (applicantToReturn == null)
                throw new NullReferenceException(_localizer["DefaultExceptionMessage"]);

            Log.Information("Applicant created: {@applicant}", applicantToReturn);
            return CreatedAtAction(nameof(Get), new { id = applicantToReturn.Id }, applicantToReturn);
        }

        // POST: ApplicantController/Edit/5
        [HttpPost("Edit/{id}")]
        [SwaggerRequestExample(typeof(UpdateApplicantDto), typeof(UpdateApplicantDtoExample))]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UpdateApplicantDto applicant)
        {
            var updatedApplicant = await _applicantService.UpdateApplicant(id, applicant);
            Log.Information("Applicant with Id {id} updated: {@applicant}", id, updatedApplicant);
            return StatusCode(StatusCodes.Status204NoContent, updatedApplicant);
        }

        // POST: ApplicantController/Delete/5
        [HttpPost("Delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _applicantService.DeleteApplicant(id);
            Log.Information("Applicant with Id {id} deleted", id);
            return Ok();
        }
    }
}
