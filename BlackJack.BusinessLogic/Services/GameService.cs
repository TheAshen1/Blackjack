using BlackJack.ViewModels.GameServiceViewModels;
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
    public class GameService
    {
        private readonly BaseRepository<Game> _gameRepository;

        public GameService(BaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<string> CreateGame(GameServiceCreateGameViewModel viewModel)
        {
            try
            {
                var result = await _gameRepository.Add(DataMapper.Map(viewModel));
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameService", "CreateGame");             
            }
            return "error";
        }

        public async Task<IEnumerable<GameServiceViewModel>> RetrieveAllGames()
        {
            try
            {
                var games = await _gameRepository.All();
                return DataMapper.Map(games);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameService", "RetrieveAllGames");               
            }
            return null;
        }

        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {
            try
            {
                if (Guid.TryParse(id, out Guid guid))
                {
                    var game = await _gameRepository.FindById(guid);
                    return DataMapper.Map(game);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameService", "RetrieveGame");

            }
            return new GameServiceViewModel
            {
                Id = Guid.Empty.ToString()
            };
        }

        public async Task<bool> UpdateGame(GameServiceViewModel viewModel)
        {
            try
            {
                var entity = DataMapper.Map(viewModel);
                if (entity.Id != Guid.Empty)
                {
                    var result = await _gameRepository.Update(entity);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameService", "UpdateGame");
                return false;
            }
            return false;
        }

        public async Task<int> DeleteGame(GameServiceViewModel viewModel)
        {
            try
            {
                var result = await _gameRepository.Remove(DataMapper.Map(viewModel));
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameService", "DeleteGame");
                return 0;
            }
        }


    }
}
