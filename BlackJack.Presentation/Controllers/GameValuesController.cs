using BlackJack.BusinessLogic.Services;
using BlackJack.Utility.Loggers;
using BlackJack.ViewModels.GameServiceViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:50000", headers: "*", methods: "*")]
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
            IEnumerable<GameServiceViewModel> result = null;
            try
            {              
                result = await _gameService.RetrieveAllGames();               
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameValueController");
            }

            return result;
        }

        [HttpGet]
        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {
            var result = await _gameService.RetrieveGame(id);
            return result;
        }

        [HttpPost]
        public async Task<string> CreateGame([FromBody]GameServiceCreateGameViewModel viewModel)
        {
            var result = await _gameService.CreateGame(viewModel);
            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateGame(string id, [FromBody]GameServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _gameService.UpdateGame(viewModel);
                return isUpdated;
            }
            return false;
        }

        
        public async Task<int> DeleteGame(string id)
        {
            var gameToDelete = await _gameService.RetrieveGame(id);
            if (gameToDelete.Id != Guid.Empty.ToString())
            {
                var result = await _gameService.DeleteGame(gameToDelete);
                return result;
            }
            return 0;
        }
    }
}
