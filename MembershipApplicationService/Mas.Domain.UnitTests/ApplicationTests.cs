using Mas.Domain.Aggregate;
using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;
using Xunit;

namespace Mas.Domain.UnitTests
{

    public class ApplicationConstructorTests
    {
        [Fact]
        public void NewApplicationSucess()
        {
            var person = new Person("Jan", "Rodes", "jan.rodes@gmail.com", "89899");
            
            var application = new Application(DateTime.Today, person, MembershipType.OnlyGym,"whateverlocation");
            
            Assert.NotEqual(Guid.Empty, application.Id);
            Assert.Equal(ApplicationStatus.Unassigned, application.Status);
            Assert.Equal("Jan", application.Person.Name.FirstName);
            Assert.Equal("Rodes", application.Person.Name.LastName);
            Assert.Equal("jan.rodes@gmail.com", application.Person.Email);
            Assert.Equal("89899", application.Person.Phone);
        }

        
    }
}
