using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ekip2.Application.DTOs.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public byte[]? Image { get; set; }
        //
        public Guid PackageId { get; set; }
        public virtual Package Package { get; set; }
    }
}
