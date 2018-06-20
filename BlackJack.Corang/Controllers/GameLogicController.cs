using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class GameLogicController : ControllerBase
    {
        private readonly GameLogicService _gameLogicService;

        public GameLogicController(GameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [HttpGet("{id}",Name = "DrawCard")]
        public async Task<string> GiveCard(string roundPlayerId)
        {
            var result = await _gameLogicService.GiveCard(roundPlayerId);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGame(string playerName, int numberOfBots)
        {

            var result = await _gameLogicService.StartNewGame(playerName, numberOfBots);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGameRound(string currentGameId)
        {
            var result = await _gameLogicService.StartNewGameRound(currentGameId);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<bool> FinishTheGame(string GameId)
        {

            var result = await _gameLogicService.FinishTheGame(GameId);
            return result;
        }
    }
}
