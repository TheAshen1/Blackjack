using BlackJack.ViewModels.RoundServiceViewModels;
using BlackJack.ViewModels.PlayerServiceViewModels;
using BlackJack.ViewModels.GameServiceViewModels;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using DataAccess.DapperModels;
using System;
using System.Collections.Generic;


namespace DataAccess.DataMappings
{
    public static class DataMapper
    {

        #region playerEntity
        public static PlayerServiceViewModel Map(Player entity)
        {
            return new PlayerServiceViewModel()
            {
                Id = entity.Id.ToString(),
                GameId = entity.GameId.ToString(),
                Name = entity.Name,
                IsBot = entity.IsBot,
                Chips = entity.Chips
                
            };
        }

        public static Player Map(PlayerServiceViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.Id, out Guid id) && Guid.TryParse(viewModel.GameId, out Guid gameId))
            {
                var entity = new Player()
                {
                    Id = id,
                    GameId = gameId,
                    Name = viewModel.Name,
                    IsBot = viewModel.IsBot,
                    Chips = viewModel.Chips
                };

                return entity;
            }

            return new Player {
                Id = Guid.Empty,
                GameId = Guid.Empty
            };

        }

        public static Player Map(PlayerServiceCreatePlayerViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.GameId, out Guid gameId))
            {
                var entity = new Player()
                {
                    Id = Guid.Empty,
                    GameId = gameId,
                    Name = viewModel.Name,
                    IsBot = viewModel.IsBot,
                    Chips = viewModel.Chips
                };

                return entity;
            }
            return new Player {
                Id = Guid.Empty,
                GameId = Guid.Empty
            };
        }

        public static IEnumerable<PlayerServiceViewModel> Map( IEnumerable<Player> entities)
        {
            var list = new List<PlayerServiceViewModel>();
            foreach (var entity in entities)
            {
                list.Add(Map(entity));
            }
            return list;
        }

        public static IEnumerable<Player> Map(IEnumerable<PlayerServiceViewModel> viewModels)
        {
            var list = new List<Player>();
            foreach (var viewModel in viewModels)
            {
                list.Add(Map(viewModel));
            }
            return list;
        }
        #endregion

        #region gameEntity
        public static GameServiceViewModel Map(Game entity)
        {
            return new GameServiceViewModel()
            {
                Id = entity.Id.ToString(),
                Start = entity.Start.ToString(),
                End = entity.End.ToString()
            };
        }

        public static Game Map(GameServiceViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.Id, out Guid id) && DateTime.TryParse(viewModel.Start, out DateTime startTime))
            {

                var entity = new Game()
                {
                    Id = id,
                    Start = startTime,

                };
                if (DateTime.TryParse(viewModel.End, out DateTime endTime))
                    entity.End = endTime;

                return entity;
            }

            return new Game {
                Id = Guid.NewGuid()
            };

        }

        public static Game Map(GameServiceCreateGameViewModel viewModel)
        {
            if (DateTime.TryParse(viewModel.Start, out DateTime startTime))
            {
                var entity = new Game()
                {
                    Id = Guid.Empty,
                    Start = startTime
                };

                return entity;
            }
            else return new Game()
            {
                Id = Guid.Empty,
                Start = DateTime.Now
            };
        }

        public static IEnumerable<GameServiceViewModel> Map(IEnumerable<Game> entities)
        {
            var list = new List<GameServiceViewModel>();
            foreach (var entity in entities)
            {
                list.Add(Map(entity));
            }
            return list;
        }

        public static IEnumerable<Game> Map(IEnumerable<GameServiceViewModel> viewModels)
        {
            var list = new List<Game>();
            foreach (var viewModel in viewModels)
            {
                list.Add(Map(viewModel));
            }
            return list;
        }

        #endregion

        #region roundEntity
        public static RoundServiceViewModel Map(Round entity)
        {
            var viewModel = new RoundServiceViewModel()
            {
                Id = entity.Id.ToString(),
                GameId = entity.GameId.ToString(),
                RoundNumber = entity.RoundNumber,
                Deck = entity.Deck
            };
            return viewModel;
        }

        public static Round Map(RoundServiceViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.Id, out Guid id) && Guid.TryParse(viewModel.GameId, out Guid gameId))
            {
                var entity = new Round()
                {
                    Id = id,
                    GameId = gameId,
                    RoundNumber = viewModel.RoundNumber,
                    Deck = viewModel.Deck
                };
                return entity;
            }
            return new Round {
                Id = Guid.Empty,
                GameId = Guid.Empty
            };

        }

        public static Round Map(RoundServiceCreateRoundViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.GameId, out Guid gameId))
            {
                var entity = new Round()
                {
                    Id = Guid.Empty,
                    GameId = gameId,
                    RoundNumber = viewModel.RoundNumber,
                    Deck = viewModel.Deck
                };


                return entity;
            }
            return new Round
            {
                Id = Guid.Empty,
                GameId = Guid.Empty
            };
        }

        public static IEnumerable<RoundServiceViewModel> Map(IEnumerable<Round> entities)
        {
            var list = new List<RoundServiceViewModel>();
            foreach (var entity in entities)
            {
                list.Add(Map(entity));
            }
            return list;
        }

        public static IEnumerable<Round> Map(IEnumerable<RoundServiceViewModel> viewModels)
        {
            var list = new List<Round>();
            foreach (var viewModel in viewModels)
            {
                list.Add(Map(viewModel));
            }
            return list;
        }

        #endregion

        #region roundPlayerEntity
        public static RoundPlayerServiceViewModel Map(RoundPlayer entity)
        {
            var viewModel =  new RoundPlayerServiceViewModel()
            {
                Id = entity.Id.ToString(),
                RoundId = entity.RoundId.ToString(),
                PlayerId = entity.PlayerId.ToString(),
                Bet = entity.Bet,
                Cards = entity.Cards
            };
            return viewModel;
        }

        public static RoundPlayer Map(RoundPlayerServiceViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.Id, out Guid id) && Guid.TryParse(viewModel.RoundId, out Guid roundId) && Guid.TryParse(viewModel.PlayerId, out Guid playerId))
            {
                var entity = new RoundPlayer()
                {
                    Id = id,
                    RoundId = roundId,
                    PlayerId = playerId,
                    Bet = viewModel.Bet,
                    Cards = viewModel.Cards
                };

                return entity;
            }

            return new RoundPlayer {
                Id = Guid.Empty,
                PlayerId = Guid.Empty,
                RoundId = Guid.Empty
            };

        }

        public static RoundPlayer Map(RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            if (Guid.TryParse(viewModel.RoundId, out Guid roundId) && Guid.TryParse(viewModel.PlayerId, out Guid playerId))
            {
                var entity = new RoundPlayer()
                {
                    Id = Guid.Empty,
                    RoundId = roundId,
                    PlayerId = playerId,
                    Bet = 0,
                    Cards = viewModel.Cards
                };

                return entity;
            }

            return new RoundPlayer
            {
                Id = Guid.Empty,
                PlayerId = Guid.Empty,
                RoundId = Guid.Empty
            };
        }


        public static IEnumerable<RoundPlayerServiceViewModel> Map(IEnumerable<RoundPlayer> entities)
        {
            var list = new List<RoundPlayerServiceViewModel>();
            foreach (var entity in entities)
            {
                list.Add(Map(entity));
            }
            return list;
        }

        public static IEnumerable<RoundPlayer> Map(IEnumerable<RoundPlayerServiceViewModel> viewModels)
        {
            var list = new List<RoundPlayer>();
            foreach (var viewModel in viewModels)
            {
                list.Add(Map(viewModel));
            }
            return list;
        }
        #endregion
    }
}
