using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    class RoundService
    {
        private readonly BaseRepository<Round> _roundRepository;
        public RoundService()
        {
            _roundRepository = new BaseRepository<Round>("Rounds", new ConnectionFactory());
        }

        public async void CreateRound(Round round)
        {
            await _roundRepository.Add(round);
        }

        public IEnumerable<Round> All()
        {
            var result = _roundRepository.All();
            return result.Result;
        }

        public async void RetrieveroundById(Guid id)
        {
            await _roundRepository.FindByID(id);
        }

        public async void Updateround(Round round)
        {
            await _roundRepository.Update(round);
        }

        public async void Deleteround(Round round)
        {
            await _roundRepository.Remove(round);
        }


    }
}
