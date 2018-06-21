using BlackJack.BusinessLogic.Services;
using BlackJack.Utility.Loggers;
using BlackJack.ViewModels.GameServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlackJack.Corang.Controllers
{
    [Route("api/[controller]")]
    public class GameValuesController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameValuesController(GameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet("[action]")]
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

        [HttpGet("[action]/{id}")]
        public async Task<GameServiceViewModel> RetrieveGame(string id)
        {
            var result = await _gameService.RetrieveGame(id);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<string> CreateGame([FromBody]GameServiceCreateGameViewModel viewModel)
        {
            var result = await _gameService.CreateGame(viewModel);
            return result;
        }

        [HttpPut("[action]/{id}")]
        public async Task<bool> UpdateGame(string id, [FromBody]GameServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _gameService.UpdateGame(viewModel);
                return isUpdated;
            }
            return false;
        }

        [HttpDelete("[action]/{id}")]
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
