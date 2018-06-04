using BlackJack.ViewModels.PlayerServiceViewModels;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataMappings
{
    public static class DataMapper
    {
       
        #region
        public static PlayerServiceViewModel Map(Player entity)
        {
            return new PlayerServiceViewModel()
            {
                Id = entity.Id.ToString(),
                Name = entity.Name,
                IsBot = entity.IsBot             
            };
        }

        public static Player Map(PlayerServiceViewModel viewModel)
        {
            var id = Guid.Empty;
            if (Guid.TryParse(viewModel.Id, out id))
            {
                var entity = new Player()
                {
                    Id = id,
                    Name = viewModel.Name,
                    IsBot = viewModel.IsBot
                };

                return entity;
            }
            else throw new ArgumentNullException();

        }

        public static Player Map(PlayerServiceCreatePlayerViewModel viewModel)
        {
            var entity = new Player()
            {
                Id = Guid.Empty,
                Name = viewModel.Name,
                IsBot = viewModel.IsBot
            };

            return entity;
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
        #endregion playerEntity
    }
}
