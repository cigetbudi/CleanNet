using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNet.Application.Interfaces
{
    public interface ICatFactService
    {
        Task<(string fact, int length)> GetCatFactAsync();
    }
}
