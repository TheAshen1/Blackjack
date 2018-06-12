using BlackJack.ViewModels.RoundPlayerServiceViewModels;
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
    public class RoundPlayerService
    {
        private readonly BaseRepository<RoundPlayer> _round_PlayerRepository;

        public RoundPlayerService(BaseRepository<RoundPlayer> round_PlayerRepository)
        {
            _round_PlayerRepository = round_PlayerRepository;
        }

        public async Task<string> CreateRoundPlayer(RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            var result = await _round_PlayerRepository.Add(DataMapper.Map(viewModel));
            return result;
        }

        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveAllRoundPlayers()
        {
            var roundPlayers = await _round_PlayerRepository.All();
            return DataMapper.Map(roundPlayers);
        }

        public async Task<RoundPlayerServiceViewModel> RetrieveRoundPlayer(string id)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                var roundPlayer = await _round_PlayerRepository.FindByID(guid);
                return DataMapper.Map(roundPlayer);
            }

            return DataMapper.Map(new RoundPlayer {
                Id = Guid.Empty
            });
        }

        public async Task<bool> UpdateRoundPlayer(RoundPlayerServiceViewModel roundPlayer)
        {
            var result = await _round_PlayerRepository.Update(DataMapper.Map(roundPlayer));
            return result;
        }

        public async Task<int> DeleteRoundPlayer(RoundPlayerServiceViewModel roundPlayer)
        {
            var result = await _round_PlayerRepository.Remove(DataMapper.Map(roundPlayer));
            return result;
        }

    }
}
