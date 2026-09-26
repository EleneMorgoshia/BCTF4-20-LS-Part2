using Microsoft.EntityFrameworkCore;
using Movie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieDbContext _movieContext;

        //dependeny injection
        public UnitOfWork(MovieDbContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task SaveChangesAsync()
        {
            await _movieContext.SaveChangesAsync();
        }

    }
}
