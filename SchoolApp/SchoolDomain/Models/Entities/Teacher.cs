using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Models.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //1 მასწს აქვს ბევრი საგანი
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    }
}


//subjects vs teacher (1 საგანს უნდა ყავდეს 1 მასწი უეჭ)
//1 subject - 1 teacer
//1 teacher - many subjects
//1 to many