using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Presentation.Controllers
{
    public class GameValuesController : ApiController
    {
        private readonly GameService _service;

        public GameValuesController(GameService service)
        {
            _service = service;
        }

        
        [HttpGet]
        public async Task<IEnumerable<GameServiceViewModel>> RetrieveAllGames()
        {
            var result = await _service.RetrieveAllGames();
            return result;
        }

        [HttpGet]
        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {
            var result = await _service.RetrieveGame(id);
            return result;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateGame([FromBody]GameServiceCreateGameViewModel viewModel)
        {
            var result = await _service.CreateGame(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateGame(string id, [FromBody]GameServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _service.UpdateGame(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        
        public async Task<HttpResponseMessage> DeleteGame(string id)
        {
            var gameToDelete = await _service.RetrieveGame(id);

            await _service.DeleteGame(gameToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
