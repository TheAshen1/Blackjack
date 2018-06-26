using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BlackJack.Corang.Controllers
{
    [Route("api/[controller]")]
    public class GameLogicController : ControllerBase
    {
        private readonly GameLogicService _gameLogicService;
        public readonly string SessionKeyPlayerId = "_PlayerId";

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
        public async Task<bool> PlaceBet([FromQuery]string roundPlayerId, [FromQuery]int bet)
        {
            var result = await _gameLogicService.PlaceBet(roundPlayerId, bet);
            return result;
        }

        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGame([FromQuery]string playerName, [FromQuery]int numberOfBots)
        {
            GameLogicViewModel result = null;
            //var userId = HttpContext.Session.GetString(SessionKeyPlayerId);
            //if (string.IsNullOrEmpty(userId))
            //{
            //    result = await _gameLogicService.StartNewGame(playerName, numberOfBots);
            //    userId = "";
            //    foreach(var player in result.Players)
            //    {
            //        if (!player.IsBot)
            //            userId = player.Id;
            //    }
            //    HttpContext.Session.SetString(SessionKeyPlayerId, userId);
            //}
            //else
            //{
            //    result = await _gameLogicService.StartNewGame(playerName, numberOfBots, userId);
            //}
            result = await _gameLogicService.StartNewGame(playerName, numberOfBots);
            return result;
        }
        [HttpGet("[action]")]
        public async Task<GameLogicViewModel> StartNewGameAuthentificated([FromQuery]string playerName, [FromQuery]int numberOfBots, [FromQuery]string userId)
        {
            GameLogicViewModel result = null;
        
            result = await _gameLogicService.StartNewGameAuthentificated(playerName, numberOfBots, userId);
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
