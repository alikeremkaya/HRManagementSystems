using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.ExpenseDTOs
{
    public class ExpenseListDTO
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; } 

        public byte[]? Image { get; set; }

        public string Description { get; set; }

        public Guid ManagerId { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public Guid CompanyId { get; set; }
        public Roles Roles { get; set; }
    }
}
