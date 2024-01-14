using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;

namespace Mas.WebApi.Models
{
    public record CreateApplicationRequest(
        DateTime DateInitiated,
        PersonDto? Person,
        MembershipType MembershipType,
        string EmailLocation
        );
    
}
