namespace Mas.Domain.ValueObjects
{
    public class Person
    {
        private Person() { }
        public Person(string firstName, string lastName, string email,
                   string phone)
        {
            Name = new PersonName(firstName, lastName);
            Phone = phone;
            Email = email;
        }

        public PersonName Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
    }
}
