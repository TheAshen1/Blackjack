using BlackJack.ViewModels.RoundServiceViewModels;
using DataAccess;
using DataAccess.DataMappings;
using DataAccess.DapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class RoundService
    {
        private readonly BaseRepository<Round> _roundRepository;

        public RoundService(BaseRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository;
        }

        public async Task<string> CreateRound(RoundServiceCreateRoundViewModel viewModel)
        {
            var result = await _roundRepository.Add(DataMapper.Map(viewModel));
            return result;
        }

        public async Task<IEnumerable<RoundServiceViewModel>> RetrieveAllRounds()
        {
            var rounds = await _roundRepository.All();
            return DataMapper.Map(rounds);
        }

        public async Task<RoundServiceViewModel> RetrieveRound(string id)
        {
            var guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                var round = await _roundRepository.FindByID(guid);
                return DataMapper.Map(round);
            }
            else throw new ArgumentNullException();// whatever, some exception is needed
        }

        public async Task<bool> UpdateRound(RoundServiceViewModel round)
        {
            var result = await _roundRepository.Update(DataMapper.Map(round));
            return result;
        }

        public async Task<int> DeleteRound(RoundServiceViewModel round)
        {
            var result = await _roundRepository.Remove(DataMapper.Map(round));
            return result;
        }


    }
}
