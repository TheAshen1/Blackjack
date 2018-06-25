using BlackJack.ViewModels.RoundServiceViewModels;
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
    public class RoundService
    {
        private readonly BaseRepository<Round> _roundRepository;

        public RoundService(BaseRepository<Round> roundRepository)
        {
            _roundRepository = roundRepository;
        }

        public async Task<string> CreateRound(RoundServiceCreateRoundViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.GameId != Guid.Empty)
                {
                    var result = await _roundRepository.Add(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "CreateRound");
            }
            return "Game does not exist!";
        }

        public async Task<IEnumerable<RoundServiceViewModel>> RetrieveAllRounds()
        {
            try
            {
                var rounds = await _roundRepository.All();
                return DataMapper.Map(rounds);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "RetrieveAllRounds");
            }
            return null;
        }

        public async Task<RoundServiceViewModel> RetrieveRound(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    var round = await _roundRepository.FindById(guid);
                    return DataMapper.Map(round);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "RetrieveRound");
            }
            return new RoundServiceViewModel
            {
                Id = Guid.Empty.ToString(),
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<bool> UpdateRound(RoundServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.GameId != Guid.Empty)
                {
                    var result = await _roundRepository.Update(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "UpdateRound");
            }
            return false;
        }

        public async Task<int> DeleteRound(RoundServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.GameId != Guid.Empty)
                {
                    var result = await _roundRepository.Remove(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "DeleteRound");
            }
            return 0;
        }

        public async Task<RoundServiceViewModel> RetrieveLastGameRound(string gameId)
        {
            try
            {
                var rounds = await _roundRepository.All();
                var result = rounds.Where(r => r.GameId.ToString() == gameId).OrderBy(r => r.RoundNumber).Last();
                return DataMapper.Map(result);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "RoundService", "RetrieveLastGameRound");
            }
            return new RoundServiceViewModel
            {
                Id = Guid.Empty.ToString(),
                GameId = Guid.Empty.ToString()
            };
        }
    }
}
