using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.PlayerServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Presentation.Controllers
{
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
        public async Task<HttpResponseMessage> CreatePlayer([FromBody]PlayerServiceCreatePlayerViewModel viewModel)
        {
            var result = await _playerService.CreatePlayer(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePlayer(string id, [FromBody]PlayerServiceViewModel viewModel)
        {
            if(id == viewModel.Id)
            {
                var isUpdated = await _playerService.UpdatePlayer(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        public async Task<HttpResponseMessage> DeletePlayer(string id)
        {
            var playerToDelete = await _playerService.RetrievePlayer(id);

            await _playerService.DeletePlayer(playerToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
