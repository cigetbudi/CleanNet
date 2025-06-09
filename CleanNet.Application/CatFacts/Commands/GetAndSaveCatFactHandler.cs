using CleanNet.Application.Interfaces;
using MediatR;

namespace CleanNet.Application.CatFacts.Commands
{
    public class GetAndSaveCatFactHandler : IRequestHandler<GetAndSaveCatFactCommand, Unit>
    {
        private readonly ICatFactService _catFactService;
        private readonly IDatabaseService _dbService;

        public GetAndSaveCatFactHandler(ICatFactService catFactService, IDatabaseService dbService)
        {
            _catFactService = catFactService;
            _dbService = dbService;
        }
        public async Task<Unit> Handle(GetAndSaveCatFactCommand request, CancellationToken cancellationToken)
        {
            (string fact, int length) = await _catFactService.GetCatFactAsync();
            await _dbService.InsertCatFactAsync(fact, length);
            return Unit.Value;
        }
    }
}
