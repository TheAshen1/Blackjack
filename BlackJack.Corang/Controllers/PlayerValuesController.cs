using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.PlayerServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class PlayerValuesController : ControllerBase
    {
        private readonly PlayerService _playerService;

        public PlayerValuesController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveAllPlayers()
        {
            var result = await _playerService.RetrieveAllPlayers();
            return result;
        }

        [HttpGet]
        public async Task<PlayerServiceViewModel> RetrievePlayer(string id)
        {
            var result = await _playerService.RetrievePlayer(id);
            return result;
        }

        [HttpPost]
        public async Task<string> CreatePlayer([FromBody]PlayerServiceCreatePlayerViewModel viewModel)
        {
            var result = await _playerService.CreatePlayer(viewModel);
            return result;
        }

        [HttpPut]
        public async Task<bool> UpdatePlayer(string id, [FromBody]PlayerServiceViewModel viewModel)
        {
            if(id == viewModel.Id)
            {
                var isUpdated = await _playerService.UpdatePlayer(viewModel);
                return isUpdated;
            }
            return false;
        }

        public async Task<int> DeletePlayer(string id)
        {
            var playerToDelete = await _playerService.RetrievePlayer(id);
            if (playerToDelete.Id != Guid.Empty.ToString())
            {
                var result = await _playerService.DeletePlayer(playerToDelete);
                return result;
            }
            return 0;
        }
    }
}
