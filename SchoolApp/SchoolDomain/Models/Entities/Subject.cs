using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Models.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        //1საგანზე რამდენი სტუდენტი არის რეგისტრირებული(1 საგანზე რამდენი ინროლმენთია განხორციელებული)
        //navigation property - ანუ ჩერეზ Enrolments კოლექციით ხდება subject-ის student-თან დაკავირება
        public ICollection<Enrolment> Enrolments = new List<Enrolment>();
    
        //1საგანს ყავს 1 მენტორი
        public int TeacherId {  get; set; } //foreign key
        public Teacher Teacher { get; set; }
    }
}


//Subject vs Enrolmetns
//1 subejct -  many students
//many student - 1 subject