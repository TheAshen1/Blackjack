using BlackJack.ViewModels.GameServiceViewModels;
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
    public class GameService
    {
        private readonly BaseRepository<Game> _gameRepository;

        public GameService(BaseRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<string> CreateGame(GameServiceCreateGameViewModel viewModel)
        {
            var result = await _gameRepository.Add(DataMapper.Map(viewModel));
            return result; 
        }

        public async Task<IEnumerable<GameServiceViewModel>> RetrieveAllGames()
        {
            var games = await _gameRepository.All();
            return DataMapper.Map(games);
        }

        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {

            var guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                var game = await _gameRepository.FindByID(guid);
                return DataMapper.Map(game);
            }
            else throw new ArgumentNullException();// whatever, some exception is needed
        }

        public async Task<bool> UpdateGame(GameServiceViewModel game)
        {
            var result = await _gameRepository.Update(DataMapper.Map(game));
            return result;
        }

        public async Task<int> DeleteGame(GameServiceViewModel game)
        {
            var result = await _gameRepository.Remove(DataMapper.Map(game));
            return result;
        }


    }
}
