using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNet.Application.Interfaces
{
    public interface IDatabaseService
    {
        Task InsertCatFactAsync(string fact, int length);
    }
}
