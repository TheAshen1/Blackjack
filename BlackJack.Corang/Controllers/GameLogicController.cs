using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BlackJack.Corang.Controllers
{
    [Route("api/[controller]")]
    public class GameLogicController : ControllerBase
    {
        private readonly GameLogicService _gameLogicService;

        public GameLogicController(GameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [HttpGet("[action]")]
        public async Task<string> GiveCard([FromQuery]string roundPlayerId)
        {
            var result = await _gameLogicService.GiveCard(roundPlayerId);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGame([FromQuery]string playerName, [FromQuery]int numberOfBots)
        {

            var result = await _gameLogicService.StartNewGame(playerName, numberOfBots);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGameRound([FromQuery]string gameId)
        {
            var result = await _gameLogicService.StartNewGameRound(gameId);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<bool> FinishTheGame([FromQuery]string gameId)
        {

            var result = await _gameLogicService.FinishTheGame(gameId);
            return result;
        }
    }
}
