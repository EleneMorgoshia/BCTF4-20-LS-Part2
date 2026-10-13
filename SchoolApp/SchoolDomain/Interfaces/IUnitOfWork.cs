using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDomain.Interfaces
{
    public interface IUnitOfWork
    {
        //თუ ეს პროცესი შეწყდა(მაგ იუზერმა რაღაც პროცესი დააქენსელა) მინდა რომ მუშაობა შეწყდეს
        //ამაში ვიყენებთ CencelationToken-ს. ანუ რომ გათიშოს ეგრევე
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
