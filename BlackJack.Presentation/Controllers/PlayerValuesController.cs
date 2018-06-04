using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.PlayerServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Presentation.Controllers
{
    public class PlayerValuesController : ApiController
    {
        private readonly PlayerService _service;

        public PlayerValuesController()
        {
            _service = new PlayerService();
        }

        public async Task<IEnumerable<PlayerServiceViewModel>> GetPlayers()
        {
            var result = await _service.All();
            return result;
        }

        public PlayerServiceViewModel GetPlayer(string id)
        {
            return _service.RetrievePlayerById(id);

        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreatePlayer([FromBody]PlayerServiceCreatePlayerViewModel viewModel)
        {
            var result = await _service.CreatePlayer(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public void EditPlayer(string id, [FromBody]PlayerServiceViewModel viewModel)
        {
            if(id == viewModel.Id)
            {
                _service.UpdatePlayer(viewModel);
            }
        }

        public void DeletePlayer(string id)
        {
            var playerToDelete = _service.RetrievePlayerById(id);

            _service.DeletePlayer(playerToDelete);
        }
    }
}
