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


                if (roundPlayer.Cards == String.Empty)
                {
                    roundPlayer.Cards = "[]";
                }

                var card = deck.Draw();
                var hand = JsonConvert.DeserializeObject<List<CardLogic>>(roundPlayer.Cards);
                hand.Add(card);
                roundPlayer.Cards = DeckLogic.Stringify(hand);
                round.Deck = deck.Stringify();

                await _roundPlayerService.UpdateRoundPlayer(roundPlayer);
                await _roundService.UpdateRound(round);
                return card.ToString();

            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "GiveCard");
                return null;
            }
        }

        public async Task<bool> PlaceBet(string roundPlayerId, int bet)
        {
            try
            {
                var roundPlayer = await _roundPlayerService.RetrieveRoundPlayer(roundPlayerId);
                var player = await _playerService.RetrievePlayer(roundPlayer.PlayerId);

                if (bet > player.Chips)
                {
                    return false;
                }

                roundPlayer.Bet = bet;

                return await _roundPlayerService.UpdateRoundPlayer(roundPlayer);
            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "PlaceBet");
                return false;
            }
        }

        private async Task<bool> ResolveBets(string roundId)
        {
            try
            {
                var round = await _roundService.RetrieveRound(roundId);
                var players = await _playerService.RetrieveGamePlayers(round.GameId);
                var roundPlayers = await _roundPlayerService.RetrieveRoundPlayersByRound(roundId);

                var Dealer = players.Where(p => p.Name == "Dealer" && p.IsBot).First();

                var maxScore = 0;
                var winners = new List<string>();

                //count scores first
                foreach (var roundPlayer in roundPlayers)
                {
                    var playerScore = CountScore(roundPlayer);
                    if (playerScore <= 21 && playerScore > maxScore)
                    {
                        winners.Clear();
                        winners.Add(roundPlayer.PlayerId);
                        maxScore = playerScore;
                    }
                    else if (playerScore == maxScore)
                    {
                        winners.Add(roundPlayer.PlayerId);
                    }
                }

                //then manage chips

              
                if (!winners.Contains(Dealer.Id.ToString()))
                {
                    foreach (var playerId in winners)
                    {
                        var roundPlayer = roundPlayers.Where(rp => rp.PlayerId == playerId).First();
                        var player = players.Where(p => p.Id == playerId).First();

                        player.Chips += (maxScore == 21 ? 3 : 1) * roundPlayer.Bet;

                        await _playerService.UpdatePlayer(player);
                    }
                }
               

                foreach (var roundPlayer in roundPlayers)
                {
                    var player = players.Where(p => p.Id == roundPlayer.PlayerId).First();
                    if (player.Id != Dealer.Id && !winners.Contains(roundPlayer.PlayerId))
                    {
                        player.Chips -= roundPlayer.Bet;
                        Dealer.Chips += roundPlayer.Bet;

                        await _playerService.UpdatePlayer(player);
                    }
                }

                return await _playerService.UpdatePlayer(Dealer); 

            }
            catch (Exception ex)
            {
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "ResolveBets");
                return false;
            }
        }

        private int CountScore(RoundPlayerServiceViewModel roundPlayer)
        {
            try
            {
                var score = 0;
                var aces = 0;

                var Cards = JsonConvert.DeserializeObject<List<CardLogic>>(roundPlayer.Cards);
                foreach (var card in Cards)
                {
                    if (!Enum.TryParse(card.value.ToString("g"), out ScoreLogic cardScore))
                    {
                        Logger.WriteLogToFile("Could not parse card", "GameLogicService", "CountScore");
                        return 0;
                    }

                    if ((int)cardScore != 1)
                    {
                        score += (int)cardScore;
                    }
                    else
                    {
                        aces++;
                    }                  
                }

                for (var i = 0; i < aces; i++)
                {
                    if (score <= 21 - (aces + score))
                    {
                        score += 11;
                    }
                    else
                    {
                        score++;
                    }
                }

                return score;
            }
            catch (Exception ex) {        
                Logger.WriteLogToFile(ex.Message, "GameLogicService", "CountScore");
            }
            return 0;
        }

        public async Task<GameLogicViewModel> StartNewGame(string playerName, int numberOfBots)
        {
            try
            {

                var newGameId = await _gameService.CreateGame(new GameServiceCreateGameViewModel());
                var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = newGameId, RoundNumber = 1, Deck = (new DeckLogic()).Stringify()});

                var players = new List<PlayerLogicViewModel> {
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = "Dealer", IsBot=true, Chips = 9000 }),
                    Name = "Dealer",
                    IsBot = true,
                    Chips = 9000,
                    Bet = 0
                },
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = playerName, IsBot=false, Chips = 100 }),
                    Name = playerName,
                    IsBot = false,
                    Chips = 100,
                    Bet = 0
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
                        Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = listOfNames[tmpIndex], IsBot = true, Chips =100 }),
                        Name = listOfNames[tmpIndex],
                        IsBot = true,
                        Chips = 100,
                        Bet = 0
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
                        await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel {
                            RoundId = newRoundId,
                            PlayerId = player.Id,                           
                            Cards = "[]"
                        });
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
            return new GameLogicViewModel
            {
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<GameLogicViewModel> StartNewGameAuthentificated(string playerName, int numberOfBots, string userId)
        {
            try
            {
                var userData = await _playerService.RetrievePlayer(userId);
                if(userData.Chips == 0)
                {
                    return await StartNewGame(playerName, numberOfBots);
                }
                ///
                var newGameId = await _gameService.CreateGame(new GameServiceCreateGameViewModel());
                var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = newGameId, RoundNumber = 1, Deck = (new DeckLogic()).Stringify() });

                var players = new List<PlayerLogicViewModel> {
                new PlayerLogicViewModel
                {
                    Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = "Dealer", IsBot=true, Chips = 9000 }),
                    Name = "Dealer",
                    IsBot = true,
                    Chips = 9000,
                    Bet = 0
                },
                new PlayerLogicViewModel
                {
                    Id = userId,
                    Name = userData.Name,
                    IsBot = false,
                    Chips = userData.Chips,
                    Bet = 0
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
                        Id = await _playerService.CreatePlayer(new PlayerServiceCreatePlayerViewModel() { GameId = newGameId, Name = listOfNames[tmpIndex], IsBot = true, Chips = 100 }),
                        Name = listOfNames[tmpIndex],
                        IsBot = true,
                        Chips = 100,
                        Bet = 0
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
                        await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel
                        {
                            RoundId = newRoundId,
                            PlayerId = player.Id,
                            Cards = "[]"
                        });
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
            return new GameLogicViewModel
            {
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<GameLogicViewModel> StartNewGameRound(string currentGameId)
        {
            try
            {
                var lastRound = await _roundService.RetrieveLastGameRound(currentGameId);
                var result = await ResolveBets(lastRound.Id);
                if (!result)
                {
                    Logger.WriteLogToFile("Could not save bets", "GameLogicService", "StartNewGameRound");
                    return new GameLogicViewModel
                    {
                        GameId = Guid.Empty.ToString()
                    };
                }
                ///
                var newRoundId = await _roundService.CreateRound(new RoundServiceCreateRoundViewModel() { GameId = currentGameId, RoundNumber = lastRound.RoundNumber + 1, Deck = (new DeckLogic()).Stringify() });

                var playerModels = await _playerService.RetrieveGamePlayers(currentGameId);

                var thePlayer = playerModels.Where(p => !p.IsBot).First();

                var Players = new List<PlayerLogicViewModel>();

                foreach (var playerModel in playerModels)
                {
                    if(playerModel.Chips > 0)
                        Players.Add(new PlayerLogicViewModel
                        {
                            Id = playerModel.Id,
                            Name = playerModel.Name,
                            IsBot = playerModel.IsBot,
                            Chips = playerModel.Chips,
                            Bet = 0
                        });
                }

                #region initialize RoundPlayer values
                for (var i = 0; i < Players.Count; i++)
                {
                    var player = Players.ElementAt(i);
                    player.CurrentRoundPlayerId =
                        await _roundPlayerService.CreateRoundPlayer(new RoundPlayerServiceCreateRoundPlayerViewModel { RoundId = newRoundId, PlayerId = player.Id, Cards = "[]" });
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
            return new GameLogicViewModel
            {
                GameId = Guid.Empty.ToString()
            };
        }

        public async Task<bool> FinishTheGame(string gameId)
        {
            try
            {
                var lastRound = await _roundService.RetrieveLastGameRound(gameId);
                var ok = await ResolveBets(lastRound.Id);
                if (!ok)
                {
                    Logger.WriteLogToFile("Could not save bets", "GameLogicService", "FinishTheGame");
                    return false;
                }
                ///
                var game = await _gameService.RetrieveGame(gameId);
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
