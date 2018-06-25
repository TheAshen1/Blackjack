using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using DataAccess;
using DataAccess.DataMappings;
using DataAccess.DapperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJack.Utility.Loggers;

namespace BlackJack.BusinessLogic.Services
{
    public class RoundPlayerService
    {
        private readonly BaseRepository<RoundPlayer> _roundPlayerRepository;

        public RoundPlayerService(BaseRepository<RoundPlayer> roundPlayerRepository)
        {
            _roundPlayerRepository = roundPlayerRepository;
        }

        public async Task<string> CreateRoundPlayer(RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.PlayerId != Guid.Empty && entity.RoundId != Guid.Empty)
                {
                    var result = await _roundPlayerRepository.Add(DataMapper.Map(viewModel));
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "CreateRoundPlayer");
            }
            return "Round or Player do not exist!";

        }

        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveAllRoundPlayers()
        {
            try
            {
                var roundPlayers = await _roundPlayerRepository.All();
                return DataMapper.Map(roundPlayers);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "RetrieveAllRoundPlayers");
            }
            return null;
        }

        public async Task<RoundPlayerServiceViewModel> RetrieveRoundPlayer(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    var roundPlayer = await _roundPlayerRepository.FindById(guid);
                    return DataMapper.Map(roundPlayer);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "RetrieveRoundPlayer");
            }
            return DataMapper.Map(new RoundPlayer {
                Id = Guid.Empty
            });
        }

        public async Task<bool> UpdateRoundPlayer(RoundPlayerServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.RoundId != Guid.Empty && entity.PlayerId != Guid.Empty)
                {
                    var result = await _roundPlayerRepository.Update(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "UpdateRoundPlayer");
            }
            return false;
        }

        public async Task<int> DeleteRoundPlayer(RoundPlayerServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.RoundId != Guid.Empty && entity.PlayerId != Guid.Empty)
                {
                    var result = await _roundPlayerRepository.Remove(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "DeleteRoundPlayer");
            }
            return 0;
        }

        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveRoundPlayersByRound(string roundId)
        {
            try{
                var roundPlayers = await _roundPlayerRepository.All();
                var result = roundPlayers.Where(rp => rp.RoundId.ToString() == roundId).ToList();
                return DataMapper.Map(result);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundPlayerService", "RetrieveRoundPlayersByRound");
            }
            return null;
        }
    }
}
