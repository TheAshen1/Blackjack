using DataAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    class GameService
    {
        private readonly BaseRepository<Game> _gameRepository;
        public GameService()
        {
            _gameRepository = new BaseRepository<Game>("Games", new ConnectionFactory());
        }

        public async void CreateGame(Game game)
        {
            await _gameRepository.Add(game);
        }

        public IEnumerable<Game> All()
        {
            var result = _gameRepository.All();
            return result.Result;
        }

        public async void RetrieveGameById(Guid id)
        {
            await _gameRepository.FindByID(id);
        }

        public async void UpdateGame(Game game)
        {
            await _gameRepository.Update(game);
        }

        public async void DeleteGame(Game game)
        {
            await _gameRepository.Remove(game);
        }


    }
}
