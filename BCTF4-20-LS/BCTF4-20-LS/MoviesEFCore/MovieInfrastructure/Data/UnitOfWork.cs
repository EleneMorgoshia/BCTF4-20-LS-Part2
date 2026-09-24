using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Infrastructure.Data
{
    public class UnitOfWork
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
