using SchoolApplication.Services.Interfaces;
using SchoolDomain.Interfaces;
using SchoolDomain.Models.DTOs;
using SchoolDomain.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolApplication.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        //get All Student
        public async Task<ICollection<StudentDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllStudentsAsync(cancellationToken); // entity type
            
            return students.Select(s => MapStudentToDTO(s))
                .ToList();
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var studentEntity = await _studentRepository.GetByIdAsync(id);
            if (studentEntity == null)
                throw new ArgumentException($"There is no student with this - {id} id");

            return  MapStudentToDTO(studentEntity);
        } 

        public async Task<StudentDTO> AddStudentAsync(CeateStudentDTO createStudentDTO, CancellationToken cancellationToken)
        {
            if(createStudentDTO == null) 
                throw new ArgumentNullException(nameof(createStudentDTO));

            var studentEntity = new Student
            {
                FirstName = createStudentDTO.FirstName,
                LastName = createStudentDTO.LastName,
                Email = createStudentDTO.Email,
                DateOfBirth = createStudentDTO.DateOfBirth
            };

            ValidateStudent(studentEntity.FirstName, studentEntity.LastName, studentEntity.Email, studentEntity.DateOfBirth);

            await _studentRepository.AddStudentAsync(studentEntity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapStudentToDTO(studentEntity);
        }

        public async Task UpdateStudentAsync(int id, UpdateStudentDTO updateStudentDTO, CancellationToken cancellationToken)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
                throw new ArgumentException($"There is no student with this {id}");
            if(updateStudentDTO == null)
                throw new ArgumentNullException(nameof(updateStudentDTO));

            ValidateStudent(updateStudentDTO.FirstName,
                updateStudentDTO.LastName,
                updateStudentDTO.Email,
                updateStudentDTO.DateOfBirth);

            student.FirstName = updateStudentDTO.FirstName;
            student.LastName = updateStudentDTO.LastName;
            student.Email = updateStudentDTO.Email;
            student.DateOfBirth = updateStudentDTO.DateOfBirth;

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
    
        }

        public async Task DeleteStudentAsnc(Student student, CancellationToken cancelationToken)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));
            if (await _studentRepository.GetByIdAsync(student.Id) == null)
                throw new ArgumentException($"There is no student with this - {student.Id} id");
            
            _studentRepository.Delete(student);
            await _unitOfWork.SaveChangesAsync(cancelationToken);
        }
        private static StudentDTO MapStudentToDTO(Student student)
        {
            return new StudentDTO
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
            };
        }

        private static void ValidateStudent(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || firstName.Length > 50)
            {
                throw new ArgumentException("Invalid first name");
            }
            if (string.IsNullOrEmpty(lastName) || lastName.Length > 50)
            {
                throw new ArgumentException("Invalid first name");
            }
            if (string.IsNullOrEmpty(email) || email.Length > 100 ||
                new EmailAddressAttribute().IsValid(email) == false)
            {
                throw new ArgumentException("Invalid first name");
            }
            if (dateOfBirth == default || dateOfBirth > DateTime.Now)
            {
                throw new ArgumentException("Invalid date of birth");
            }
        }
    }
}
