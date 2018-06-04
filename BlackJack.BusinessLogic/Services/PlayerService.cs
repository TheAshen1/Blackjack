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
        public PlayerService()
        {
            _playerRepository = new BaseRepository<Player>("Players", new ConnectionFactory());
        }

        public async Task<int> CreatePlayer(PlayerServiceCreatePlayerViewModel player)
        {
           var result  =  await _playerRepository.Add( DataMapper.Map(player));
            return result;
        }

        public async Task<IEnumerable<PlayerServiceViewModel>> All()
        {
            var players = await _playerRepository.All();
            return DataMapper.Map(players);
        }

        public PlayerServiceViewModel RetrievePlayerById(string id)
        {
            var guid = Guid.Empty;
            if (Guid.TryParse(id, out guid))
            {
                var player = _playerRepository.FindByID(guid);
                return DataMapper.Map(player.Result);
            }
            else throw new ArgumentNullException();
        }

        public async void UpdatePlayer(PlayerServiceViewModel player)
        {
            await _playerRepository.Update(DataMapper.Map(player)); 
        }

        public async void DeletePlayer(PlayerServiceViewModel player)
        {
            await _playerRepository.Remove(DataMapper.Map(player));
        }



    }
}
