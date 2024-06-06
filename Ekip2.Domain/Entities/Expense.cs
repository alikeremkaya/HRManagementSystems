using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Domain.Entities;

public class Expense : AuditableEntity
{
    public double Amount { get; set; }
    public DateTime ExpenseDate { get; set; } 

    public byte[]? Image { get; set; }

    public string Description { get; set; }

    //NAV
    public Guid ManagerId { get; set; }
    public virtual Manager Manager { get; set; }


}
