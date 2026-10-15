using SchoolDomain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Interfaces
{
    public interface  IStudentRepository
    {
        Task<ICollection<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default);
        Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddStudentAsync(Student student, CancellationToken cancellationToken = default);
        public void Update(Student student);
        void Delete(Student student);
    }
}
