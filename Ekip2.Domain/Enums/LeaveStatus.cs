using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Domain.Enums
{
    public enum LeaveStatus
    {
        Pending = 1,  // Bekliyor
        Approved = 2, // Onaylandı
        Rejected = 3  // Reddedildi
    }
}
