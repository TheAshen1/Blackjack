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
        private readonly GameService _gameService;

        public GameValuesController(GameService gameService)
        {
            _gameService = gameService;
        }

        
        [HttpGet]
        public async Task<IEnumerable<GameServiceViewModel>> RetrieveAllGames()
        {
            var result = await _gameService.RetrieveAllGames();
            return result;
        }

        [HttpGet]
        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {
            var result = await _gameService.RetrieveGame(id);
            return result;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateGame([FromBody]GameServiceCreateGameViewModel viewModel)
        {
            var result = await _gameService.CreateGame(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateGame(string id, [FromBody]GameServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _gameService.UpdateGame(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }

        
        public async Task<HttpResponseMessage> DeleteGame(string id)
        {
            var gameToDelete = await _gameService.RetrieveGame(id);

            await _gameService.DeleteGame(gameToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
