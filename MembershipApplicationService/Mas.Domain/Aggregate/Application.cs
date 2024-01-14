using Mas.Domain.Enums;
using Mas.Domain.ValueObjects;
using System.Security.Cryptography;

namespace Mas.Domain.Aggregate
{
    public class Application
    {
        private Application() { }
        public Application(DateTime initDate, Person person, MembershipType membershipType, string emailLocation) 
        {
            _initiated = initDate;
            Person = person;
            Id = Guid.NewGuid();
            MembershipType = membershipType;
            Status = ApplicationStatus.Unassigned;
            EmailLocation = emailLocation;
            AssignToUserId = string.Empty;

        }

        public Guid Id { get; private set; }
        public string AssignToUserId { get; private set; }
        
        public string EmailLocation { get; private set; }


        public DateTime DateInitiated => _initiated;

        public Person Person { get; private set; }

        public MembershipType MembershipType { get; private set; }
        public ApplicationStatus Status { get; private set; }
        
        public ReferralProcessInfo? ReferralProcessInfo { get; private set; }



        private DateTime _initiated;

        public void UpdateDetails(Person person, MembershipType membershipType)
        {
            if (Status == ApplicationStatus.Assigned)
            {
                Person = person;
                MembershipType = membershipType;
            }
            else
                throw new InvalidOperationException("A decision can't be taken");
            
        }

        public void Assign(string userId)
        {
            if (Status == ApplicationStatus.Unassigned)
            {
                AssignToUserId = userId;
                Status = ApplicationStatus.Assigned;
            }
            else
                throw new InvalidOperationException("An assignation can't be made");

        }

        public void Unassign()
        {
            if (Status == ApplicationStatus.Assigned)
            {
                AssignToUserId = string.Empty;
                Status = ApplicationStatus.Unassigned;
            }
            else
                throw new InvalidOperationException("An unassignation can't be made");

        }

        public void Refer()
        {
            if (Status == ApplicationStatus.Assigned)
            {
                AssignToUserId = string.Empty;
                Status = ApplicationStatus.Referred;
                ReferralProcessInfo = new ReferralProcessInfo();
            }
            else
                throw new InvalidOperationException("It can't be referred");
        }

        public void ReferralCompleted(string emailNewLocation)
        {
            if (Status == ApplicationStatus.Referred)
            {
                ReferralProcessInfo.UpdateOnEmailMoved(emailNewLocation);
                
            }
            else
                throw new InvalidOperationException("It can't complete the referral");
        }


    }
}
