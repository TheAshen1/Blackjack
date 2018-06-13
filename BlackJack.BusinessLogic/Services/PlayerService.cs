using BlackJack.ViewModels.PlayerServiceViewModels;
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
    public class PlayerService
    {
        private readonly BaseRepository<Player> _playerRepository;

        public PlayerService(BaseRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<string> CreatePlayer(PlayerServiceCreatePlayerViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.GameId != Guid.Empty)
                {
                    var result = await _playerRepository.Add(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }
            return "Game does not exist!";
        }

        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveAllPlayers()
        {
            try
            {
                var players = await _playerRepository.All();
                return DataMapper.Map(players);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }
            return null;

        }

        public async Task<PlayerServiceViewModel> RetrievePlayer(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    var player = await _playerRepository.FindById(guid);
                    return DataMapper.Map(player);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }

            return new PlayerServiceViewModel
            {
                Id = Guid.Empty.ToString(),
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<bool> UpdatePlayer(PlayerServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.GameId != Guid.Empty)
                {
                    var result = await _playerRepository.Update(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }
            return false;
        }

        public async Task<int> DeletePlayer(PlayerServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty && entity.GameId != Guid.Empty)
                {
                    var result = await _playerRepository.Remove(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }
            return 0;
        }


        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveGamePlayers(string gameId)
        {
            try
            {
                var players = await _playerRepository.All();
                var result = players.Where(p => p.GameId.ToString() == gameId).ToList();
                return DataMapper.Map(result);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "PlayerService");
            }
            return null;
        }
    }
}
