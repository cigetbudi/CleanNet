using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNet.Application.CatFacts.Commands
{
    public class GetAndSaveCatFactCommand : IRequest<Unit> { }
  
}
