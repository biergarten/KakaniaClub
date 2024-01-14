using Mas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas.Domain.ValueObjects
{
    public class ReferralProcessInfo
    {
        public ReferralProcessInfo()
        {
            ReferralStatus = ReferralStatus.PendingEmailMove;
            
        }

        public void UpdateOnEmailMoved(string emailNewLocation)
        {
            ReferralStatus = ReferralStatus.Completed;
            HasEmailBeenMoved = true;
            EmailNewLocation = emailNewLocation;
        }

        public ReferralStatus ReferralStatus { get; private set; }
        public bool HasEmailBeenMoved { get; private set; } = false;

        public string EmailOriginalLocation { get; private set; }

        public string EmailNewLocation { get; private set; } = string.Empty;
    }
}
