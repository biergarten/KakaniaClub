using Azure.Core;
using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;
using Mas.Infrastructure.Data;
using Mas.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mas.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipApplicationController : ControllerBase
    {
        private readonly IApplicationRepository _applicationRepository;

        public MembershipApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        // GET: api/<MembershipApplication>
        [HttpGet]
        public async Task<ActionResult<ListApplicationResponse>> Get(ApplicationStatus status = ApplicationStatus.Unassigned)
        {
            var applications = await _applicationRepository.GetAsync(status);
            return Ok(applications.Select(application=> application.ToDto()));
        }



        // GET api/<MembershipApplication>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetApplicationResponse>> Get(Guid id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            if (application == null)
                return NotFound();
            var response = new GetApplicationResponse(application.ToDto()
                );
            return Ok(response);
        }

        // POST api/<MembershipApplication>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateApplicationRequest request)
        {
            var application = new Application(
                request.DateInitiated,
                request.Person.ToDomainEntity(),
                request.MembershipType,
                request.EmailLocation); ;
            await _applicationRepository.AddAsync(application);
            await _applicationRepository.SaveChangesAsync();
            return Created();
        }

        // PUT api/<MembershipApplication>/5
        [HttpPut("{id}/assign/{userId}")]
        public async Task<ActionResult> PutAssign(Guid id, string userId)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            application.Assign(userId);
            await _applicationRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("assign/{userId}")]
        public async Task<ActionResult<GetApplicationResponse>> PutAssignAvailable(string userId)
        {
            var application = await _applicationRepository.GetFirstUnassignedAsync(userId);
            if (application == null)
                return NoContent();
          
            if(application.AssignToUserId == userId)
                return Ok(application);
            
            
            application.Assign(userId);
            await _applicationRepository.SaveChangesAsync();

            return Ok(application);
        }

        [HttpPut("{id}/unassign")]
        public async Task<ActionResult> PutUnassign(Guid id)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            application.Unassign();
            await _applicationRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}/refer")]
        public async Task<ActionResult> PutRefer(Guid id, [FromBody] UpdateApplicationRequest request)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            application.UpdateDetails(request.Person.ToDomainEntity(),
                request.MembershipType);
            application.Refer();
            await _applicationRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}/referral")]
        public async Task<ActionResult> PutRefer(Guid id, [FromBody] UpdateReferralRequest request)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            application.ReferralCompleted(request.EmailNewLocation);
            await _applicationRepository.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<MembershipApplication>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateApplicationRequest request)
        {
            var application = await _applicationRepository.GetByIdAsync(id);
            application.UpdateDetails(request.Person.ToDomainEntity(),
                request.MembershipType);
            await _applicationRepository.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<MembershipApplication>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _applicationRepository.DeleteAsync(id);
            await _applicationRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
