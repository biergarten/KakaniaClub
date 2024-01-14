using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;

namespace Mas.WebApi.Models
{
    public record ApplicationDto(
        Guid id,
        string AssignToUserId,
        DateTime DateInitiated,
       Person Person,
       MembershipType MembershipType,
       string EmailLocation,
       ApplicationStatus Status,
       ReferralProcessInfo? ReferralProcessInfo

       );

    public static class ApplicationDtoExtensions
    {
        public static ApplicationDto ToDto(this Application application)
        {
            return new ApplicationDto(
                application.Id,
                application.AssignToUserId,
                application.DateInitiated,
                application.Person,
                application.MembershipType,
                application.EmailLocation,
                application.Status,
                application.ReferralProcessInfo
                );
        }
    }
}
