using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlackJack.Presentation.Controllers
{
    [EnableCors(origins: "http://localhost:59977", headers: "*", methods: "*")]
    public class GameLogicController : ApiController
    {
        private readonly GameLogicService _gameLogicService;

        public GameLogicController(GameLogicService gameLogicService)
        {
            _gameLogicService = gameLogicService;
        }

        [HttpGet]
        public async Task<string> GiveCard(string roundPlayerId)
        {
            var result = await _gameLogicService.GiveCard(roundPlayerId);
            return result;
        }

        [HttpGet]
        public async Task<GameLogicViewModel> StartNewGame(string playerName, int numberOfBots)
        {

            var result = await _gameLogicService.StartNewGame(playerName, numberOfBots);
            return result;
        }

        [HttpGet]
        public async Task<GameLogicViewModel> StartNewGameRound(string currentGameId)
        {
            var result = await _gameLogicService.StartNewGameRound(currentGameId);
            return result;
        }

        [HttpGet]
        public async Task<bool> FinishTheGame(string GameId)
        {

            var result = await _gameLogicService.FinishTheGame(GameId);
            return result;
        }
    }
}
