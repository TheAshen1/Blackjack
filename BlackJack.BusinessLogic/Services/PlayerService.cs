using BlackJack.ViewModels.PlayerServiceViewModels;
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
    public class PlayerService
    {
        private readonly BaseRepository<Player> _playerRepository;

        public PlayerService(BaseRepository<Player> playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<string> CreatePlayer(PlayerServiceCreatePlayerViewModel player)
        {
            var entity = DataMapper.Map(player);
            if (entity.Id != Guid.Empty)
            {
                var result = await _playerRepository.Add(DataMapper.Map(player));
                return result;
            }
            return "Game does not exist!";
        }

        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveAllPlayers()
        {
            var players = await _playerRepository.All();
            return DataMapper.Map(players);
        }

        public async Task<PlayerServiceViewModel> RetrievePlayer(string id)
        {
            if (Guid.TryParse(id, out Guid guid))
            {
                var player = await _playerRepository.FindByID(guid);
                return DataMapper.Map(player);
            }

            return DataMapper.Map(new Player{
                Id = Guid.Empty
            });
        }

        public async Task<bool> UpdatePlayer(PlayerServiceViewModel player)
        {
            var entity = DataMapper.Map(player);
            if (entity.Id != Guid.Empty)
            {
                var result = await _playerRepository.Update(DataMapper.Map(player));
                return result;
            }
            return false;
        }

        public async Task<int> DeletePlayer(PlayerServiceViewModel player)
        {
            var entity = DataMapper.Map(player);
            if (entity.Id != Guid.Empty)
            {
                var result = await _playerRepository.Remove(DataMapper.Map(player));
                return result;
            }
            return 0;
        }


        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveGamePlayers(string gameId)
        {
            var players = await _playerRepository.All();
            var result = players.Where(p => p.GameId.ToString() == gameId).ToList();
            return DataMapper.Map(result);
        }
    }
}
