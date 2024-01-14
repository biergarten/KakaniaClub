using Mas.Domain.ValueObjects;
using System.Numerics;

namespace Mas.WebApi.Models
{
    public class PersonDto()
    {
        public PersonNameDto Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
    public class PersonNameDto()
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static class PersonDtoExtensions
    {
        public static PersonDto ToDto(this Person person)
        {
            return new PersonDto
            {
                Phone = person.Phone,
                Email = person.Email,
                Name=new PersonNameDto()
                { FirstName = person.Name.FirstName, LastName = person.Name.LastName }
            };
        }

        public static Person ToDomainEntity(this PersonDto personDto)
        {
            return new Person(personDto.Name.FirstName, personDto.Name.LastName, personDto.Email,
                   personDto.Phone);
        }
    }
}





