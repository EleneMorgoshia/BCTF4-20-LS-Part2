using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Email {  get; set; }
        public DateTime DateOfBirth { get; set; }
        //public StudentProfile StudentProfileId {  get; set; } //foreign key

        //სტუდენტი რამდენ საგანზე არის დარეგისტრირებული მაგისი კოლექცია(ანუ სტუდენტის ინროლმენთის მთლიანი ისტორია)
        //navigation property - ანუ ჩერეზ Enrolments კოლექციით ხდება student-ის subject-თან დაკავირება
        public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();


        //Student vs StudentProfile
        public StudentProfile StudentProfile { get; set; }
    }
}



//student vs subjects
//1st - many subject
//1subj - many student 
//many to many