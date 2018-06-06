using BlackJack.ViewModels.PlayerServiceViewModels;
using DataAccess;
using DataAccess.DataMappings;
using DataAccess.Models;
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
            var result =  await _playerRepository.Add( DataMapper.Map(player));
            return result;
        }

        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveAllPlayers()
        {
            var players = await _playerRepository.All();
            return DataMapper.Map(players);
        }

        public async Task<PlayerServiceViewModel> RetrievePlayer(string id)
        {
            var guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                var player = await _playerRepository.FindByID(guid);
                return DataMapper.Map(player);
            }
            else throw new ArgumentNullException();// whatever, some exception is needed
        }

        public async Task<bool> UpdatePlayer(PlayerServiceViewModel player)
        {
            var result =  await _playerRepository.Update(DataMapper.Map(player));
            return result;
        }

        public async Task<int> DeletePlayer(PlayerServiceViewModel player)
        {
            var result = await _playerRepository.Remove(DataMapper.Map(player));
            return result;
        }



    }
}
