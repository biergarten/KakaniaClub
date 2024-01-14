using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;

namespace Mas.WebApi.Models
{
    public record UpdateApplicationRequest(
        DateTime DateInitiated,
        Person Person,
        MembershipType MembershipType
        );
}
