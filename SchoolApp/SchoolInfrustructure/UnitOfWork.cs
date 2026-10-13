using SchoolDomain.Interfaces;
using SchoolInfrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolInfrustructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolDbContext _schoolDbContext;
        public UnitOfWork(SchoolDbContext schoolDbContext)
        {
            _schoolDbContext = schoolDbContext;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _schoolDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
