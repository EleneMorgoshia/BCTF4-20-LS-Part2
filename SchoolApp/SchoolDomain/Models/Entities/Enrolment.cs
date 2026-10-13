using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Models.Entities
{
    public class Enrolment
    {
        //ეს კლასი აღწერს კონკრეტულ სტუდენტი რა საგანზე არის რეგისტრირებული
        public int Id { get; set; }

        public int StudentId { get; set; } // student foreign key
        public int SubjectId {  get; set; } // subejct foreign key

        public Student Student { get; set; }   //navigation property - ანუ enrolment კლასი ჩერეზ ამ ფროფერთით უკავშირდება Student კლასს 
        public Subject Subject { get; set; } // navigation property - enrolment კლასი ჩერეზ ამ ფროფერთით უკავშირდება Subject  კლასს
        public DateTime EnrolmentDate { get; set; }

    }
}


//Student vs Enrolment
//1 student - many enrolments(different subjects)
//1enrolment(registration r ecord) - 1 student