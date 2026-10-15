using SchoolDomain.Models.DTOs;
using SchoolDomain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApplication.Services.Interfaces
{
    public interface IStudentService
    {
        Task<ICollection<StudentDTO>> GetAllAsync(CancellationToken cancellationToken);
        Task<StudentDTO> GetStudentByIdAsync(int id, CancellationToken cancellationToken);
        Task<StudentDTO> AddStudentAsync(CeateStudentDTO createStudentDTO, CancellationToken cancellationToken);
        Task UpdateStudentAsync(int id, UpdateStudentDTO updateStudentDTO, CancellationToken cancellationToken);
        Task DeleteStudentAsnc(int id, CancellationToken cancelationToken);
    }
}
