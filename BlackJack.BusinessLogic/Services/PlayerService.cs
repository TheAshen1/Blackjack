using DataAccess;
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

        public async void CreatePlayer(Player player)
        {
            await _playerRepository.Add(player);
        }

        public IEnumerable<Player> All()
        {
            var result =  _playerRepository.All();
            return result.Result;
        }

        public async void RetrievePlayerById(Guid id)
        {
            await _playerRepository.FindByID(id);
        }

        public async void UpdatePlayer(Player player)
        {
            await _playerRepository.Update(player);
        }

        public async void DeletePlayer(Player player)
        {
            await _playerRepository.Remove(player);
        }



    }
}
