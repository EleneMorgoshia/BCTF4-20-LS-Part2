using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Models.Entities
{
    public class StudentProfile
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }

        public int StudentId { get; set; } //foreign key
        public Student Student { get; set; }//navgation property
    }
}
