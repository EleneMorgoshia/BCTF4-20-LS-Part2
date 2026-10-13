using Microsoft.EntityFrameworkCore;
using SchoolDomain.Interfaces;
using SchoolDomain.Models.Entities;
using SchoolInfrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolInfrustructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        //dependency injection
        //უნდა მივწვდე მონაცემთა ბაზას რაღაცნაირად
        private readonly SchoolDbContext _schoolDbContext;
        private readonly IUnitOfWork _unitOfWork;
        public StudentRepository(SchoolDbContext schoolDbContext, IUnitOfWork unitOfWork)
        {
            _schoolDbContext = schoolDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Student>> GetAllStudentsAsync(CancellationToken cancellationToken = default)
        {
            return await _schoolDbContext.Students.ToListAsync(cancellationToken);
        }
    
        public async Task<Student> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _schoolDbContext.Students.FirstOrDefaultAsync(s => s.Id == id,
               cancellationToken);
            //unit of work patter is used to save changes in database
        }

        public async Task AddStudentAsync(Student student, CancellationToken cancellationToken = default)
        {
            await _schoolDbContext.Students.AddAsync(student, cancellationToken);
        }

        public void Update(Student student)
        {
            _schoolDbContext.Students.Update(student);
        }

        public void Delete(Student student) 
        {
            _schoolDbContext.Students.Remove(student);
        }
    }
}
