using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICAR.Scanner.DataAccess.Models
{
    public partial class Institutions
    {
        public int InstitutionID { get; set; }
        public string InstitutionName { get; set; }
        public string? InstitutionHead { get; set; }
        public string? InstitutionAdress { get; set; }
        public bool? Status { get; set; }
        public DateOnly? CreatedOn { get; set; }
        public DateOnly? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
