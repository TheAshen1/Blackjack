using BlackJack.BusinessLogic.GameLogic;
using BlackJack.Utility.Loggers;
using BlackJack.ViewModels.GameLogicViewModels;
using BlackJack.ViewModels.GameServiceViewModels;
using BlackJack.ViewModels.PlayerServiceViewModels;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using BlackJack.ViewModels.RoundServiceViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.Services
{
    public class GameLogicService
    {
        private readonly GameService _gameService;
        private readonly PlayerService _playerService;
        private readonly RoundService _roundService;
        private readonly RoundPlayerService _roundPlayerService;

        public GameLogicService(GameService gameService, PlayerService playerService, RoundService roundService, RoundPlayerService roundPlayerService)
        {
            _gameService = gameService;
            _playerService = playerService;
            _roundService = roundService;
            _roundPlayerService = roundPlayerService;
        }


        public async Task<string> GiveCard(string roundPlayerId)
        {
            try
            {
                var roundPlayer = await _roundPlayerService.RetrieveRoundPlayer(roundPlayerId);
                var round = await _roundService.RetrieveRound(roundPlayer.RoundId);
                var deck = new DeckLogic(JsonConvert.DeserializeObject<List<CardLogic>>(round.Deck));


                var card = deck.Draw().ToString();
                roundPlayer.Cards += card + ", ";
                round.Deck = deck.Stringify();

                await _roundPlayerService.UpdateRoundPlayer(roundPlayer);
                await _roundService.UpdateRound(round);
                return card;

            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "GiveCard");
                return null;
            }
        }

        public async Task<GameLogicViewModel> StartNewGame(string playerName, int numberOfBots)
        {
            try
            {
                var newGameId = await _gameService.CreateGame(new GameServiceCreateGameViewModel());
                var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = newGameId, Deck = (new DeckLogic()).Stringify() });

                var players = new List<PlayerLogicViewModel> {
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = "Dealer", IsBot=true }),
                    Name = "Dealer",
                    IsBot = true

                },
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = playerName, IsBot=false }),
                    Name = playerName,
                    IsBot = false
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
                    var bot = new PlayerLogicViewModel
                    {
                        Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = listOfNames[tmpIndex], IsBot = true }),
                        Name = listOfNames[tmpIndex],
                        IsBot = true
                    };
                    listOfNames.RemoveAt(tmpIndex);

                    players.Add(bot);
                }
                #endregion

                #region initialize RoundPlayer values
                for (var i = 0; i < players.Count; i++)
                {
                    var player = players.ElementAt(i);
                    player.CurrentRoundPlayerId =
                        await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel { RoundId = newRoundId, PlayerId = player.Id, Cards = "" });
                }
                #endregion

                var viewModel = new GameLogicViewModel();

                viewModel.GameId = newGameId;
                viewModel.CurrentRoundId = newRoundId;
                viewModel.Players = players;

                return viewModel;
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "StartNewGame");
            }
            return new GameLogicViewModel {
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<GameLogicViewModel> StartNewGameRound(string currentGameId)
        {
            try
            {
                var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = currentGameId, Deck = (new DeckLogic()).Stringify() });

                var playerModels = await _playerService.RetrieveGamePlayers(currentGameId);

                var Players = new List<PlayerLogicViewModel>();

                foreach (var playerModel in playerModels)
                {
                    Players.Add(new PlayerLogicViewModel
                    {
                        Id = playerModel.Id,
                        Name = playerModel.Name,
                        IsBot = playerModel.IsBot
                    });
                }

                #region initialize RoundPlayer values
                for (var i = 0; i < Players.Count; i++)
                {
                    var player = Players.ElementAt(i);
                    player.CurrentRoundPlayerId =
                        await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel { RoundId = newRoundId, PlayerId = player.Id, Cards = "" });
                }
                #endregion

                var viewModel = new GameLogicViewModel();

                viewModel.GameId = currentGameId;
                viewModel.CurrentRoundId = newRoundId;
                viewModel.Players = Players;

                return viewModel;
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "StartNewGameRound");
            }
            return new GameLogicViewModel {
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<bool> FinishTheGame(string GameId)
        {
            try
            {
                var game = await _gameService.RetrieveGame(GameId);
                game.End = DateTime.Now.ToString();
                var result = await _gameService.UpdateGame(game);
                return result;
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "FinishTheGame");
            }
            return false;
        }
    }
}
