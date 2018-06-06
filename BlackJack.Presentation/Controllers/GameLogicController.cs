using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
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
    public class GameLogicController : ApiController
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private readonly RoundService _roundService;
        private readonly RoundPlayerService _roundPlayerService;

        public GameLogicController(GameService gameService, PlayerService playerService, RoundService roundService, RoundPlayerService roundPlayerService)
        {
            _gameService = gameService;
            _playerService = playerService;
            _roundService = roundService;
            _roundPlayerService = roundPlayerService;
        }

        public IEnumerable<string> GetPlayers()
        {
            return new string[] { "value1", "value2" };
        }


        public string GetPlayer(string PlayerId)
        {
            return "value";
        }


        [HttpPost]
        public async Task<GameViewModel> StartNewGameAsync([FromBody]StartGameViewModel createViewModel)
        {
            var newGameId = await _gameService.CreateGame(new GameServiceCreateGameViewModel());

            var viewModel = new GameViewModel()
            {

            };
            return viewModel;
        }


        [HttpPut]
        public void GiveCardToPlayer(string playerId)
        {
        }

        [HttpDelete]
        public void MoveToTheNextRound(int id)
        {
        }
    }
}
