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
        private readonly PlayerService _service;

        public PlayerValuesController(PlayerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerServiceViewModel>> RetrieveAllPlayers()
        {
            var result = await _service.RetrieveAllPlayers();
            return result;
        }

        [HttpGet]
        public async Task<PlayerServiceViewModel> RetrievePlayer(string id)
        {
            var result = await _service.RetrievePlayer(id);
            return result;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreatePlayer([FromBody]PlayerServiceCreatePlayerViewModel viewModel)
        {
            var result = await _service.CreatePlayer(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePlayer(string id, [FromBody]PlayerServiceViewModel viewModel)
        {
            if(id == viewModel.Id)
            {
                var isUpdated = await _service.UpdatePlayer(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        public async Task<HttpResponseMessage> DeletePlayer(string id)
        {
            var playerToDelete = await _service.RetrievePlayer(id);

            await _service.DeletePlayer(playerToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
