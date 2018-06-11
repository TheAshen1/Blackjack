using BlackJack.BusinessLogic.GameLogic;
using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.GameLogicViewModels;
using BlackJack.ViewModels.GameServiceViewModels;
using BlackJack.ViewModels.PlayerServiceViewModels;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using BlackJack.ViewModels.RoundServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

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

        [HttpGet]
        public async Task<string> GiveCard(string roundPlayerId)
        {
            var roundPlayer = await _roundPlayerService.RetrieveRoundPlayer(roundPlayerId);
            var round = await _roundService.RetrieveRound(roundPlayer.RoundId);
            var deck = new DeckLogic( new JavaScriptSerializer().Deserialize<List<CardLogic>>(round.Deck));


            var card = deck.Draw().ToString();
            roundPlayer.Cards += card + ", ";
            round.Deck = deck.Stringify();

            await _roundPlayerService.UpdateRoundPlayer(roundPlayer);
            await _roundService.UpdateRound(round);

            return card;
        }

        //[HttpGet]
        //public async Task<string> GiveTwoCards(string roundPlayerId)
        //{
        //    var roundPlayer = await _roundPlayerService.RetrieveRoundPlayer(roundPlayerId);
        //    var round = await _roundService.RetrieveRound(roundPlayer.RoundId);
        //    var deck = new JavaScriptSerializer().Deserialize<DeckLogic>(round.Deck);


        //    var card1 = _deck.Draw().ToString();
        //    var card2 = _deck.Draw().ToString();
        //    roundPlayer.Cards += card1;
        //    roundPlayer.Cards += card2;
        //    round.Deck = new JavaScriptSerializer().Serialize(deck);

        //    await _roundPlayerService.UpdateRoundPlayer(roundPlayer);
        //    await _roundService.UpdateRound(round);

            
        //    return card1 + "," + card2 ;
        //}

        [HttpPost]
        public async Task<bool> EndTheGame([FromBody]EndGameDataViewModel viewModel)
        {
            var game = await _gameService.RetrieveGame(viewModel.GameId);
            game.End = DateTime.Now.ToString();
            var result = await _gameService.UpdateGame(game);
            return result;
        }


        [HttpGet]
        public async Task<GameLogicViewModel> StartNewGameAsync(string playerName, int numberOfBots)
        {

            var newGameId = await _gameService.CreateGame(new GameServiceCreateGameViewModel());
            var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = newGameId, Deck = (new DeckLogic()).Stringify() });

            var Players = new List<PlayerLogicViewModel> {
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = "Dealer", IsBot=true }),
                    Name = "Dealer",
                    IsBot = true,
                    Cards = new List<string>()

                },
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = playerName, IsBot=false }),
                    Name = playerName,
                    IsBot = false,
                    Cards = new List<string>()
                }
            };

            #region create Bots
            var listOfNames = new List<string>()
            {
                "James",
                "John",
                "Robert",
                "Michael",
                "William",
                "David",
                "Richard",
                "Charles",
                "Joseph",
                "Thomas",
                "Christopher",
                "Daniel",
                "Paul",
                "Mark",
                "Donald",
            };
            var RAND = new Random();

            foreach (var name in listOfNames)
            {
                if (name == playerName)
                {
                    listOfNames.Remove(name);
                }
            }

            for (var i = 0; i < numberOfBots; i++)
            {
                var tmpIndex = RAND.Next(listOfNames.Count);
                var bot = new PlayerLogicViewModel{
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = listOfNames[tmpIndex], IsBot = true }),
                    Name = listOfNames[tmpIndex],
                    IsBot = true,
                    Cards = new List<string>()
                };
                listOfNames.RemoveAt(tmpIndex);

                Players.Add(bot);
            }
            #endregion

            #region initialize RoundPlayer values
            for(var i=0; i < Players.Count; i++)
            {
                var player = Players.ElementAt(i);
                player.CurrentRoundPlayerId = 
                    await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel { RoundId = newRoundId, PlayerId = player.Id, Cards="" });
            }
            #endregion

            var viewModel = new GameLogicViewModel()
            {
                GameId = newGameId,
                CurrentRoundId = newRoundId,
                Players = Players
            };
            return viewModel;
        }


        //[HttpPut]
        //public async Task<HttpResponseMessage> MoveToTheNextRound([FromBody] GameLogicViewModel gameViewModel)
        //{
            
        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
