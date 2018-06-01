using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    class RoundPlayerService
    {
        private readonly BaseRepository<Round_Player> _round_PlayerRepository;

        public RoundPlayerService()
        {
            _round_PlayerRepository = new BaseRepository<Round_Player>("Round_Player", new ConnectionFactory());
        }

        public async void CreateRound_Player(Round_Player Round_Player)
        {
            await _round_PlayerRepository.Add(Round_Player);
        }

        public IEnumerable<Round_Player> All()
        {
            var result = _round_PlayerRepository.All();
            return result.Result;
        }

        public async void RetrieveRound_PlayerById(Guid id)
        {
            await _round_PlayerRepository.FindByID(id);
        }

        public async void UpdateRound_Player(Round_Player Round_Player)
        {
            await _round_PlayerRepository.Update(Round_Player);
        }

        public async void DeleteRound_Player(Round_Player Round_Player)
        {
            await _round_PlayerRepository.Remove(Round_Player);
        }

    }
}
