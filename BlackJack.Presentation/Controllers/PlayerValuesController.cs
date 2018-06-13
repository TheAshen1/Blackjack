using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.PlayerServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:59977", headers: "*", methods: "*")]
    public class PlayerValuesController : ApiController
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
