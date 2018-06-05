using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Presentation.Controllers
{
    public class RoundPlayerValuesController : ApiController
    {
        private readonly RoundPlayerService _service;

        public RoundPlayerValuesController(RoundPlayerService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveAllRounds()
        {
            var result = await _service.RetrieveAllRoundPlayers();
            return result;
        }

        [HttpGet]
        public async Task<RoundPlayerServiceViewModel> RetrieveRound(string id)
        {
            var result = await _service.RetrieveRoundPlayer(id);
            return result;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateRound([FromBody]RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            var result = await _service.CreateRoundPlayer(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateRound(string id, [FromBody]RoundPlayerServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _service.UpdateRoundPlayer(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }


        public async Task<HttpResponseMessage> DeleteRound(string id)
        {
            var roundPlayerToDelete = await _service.RetrieveRoundPlayer(id);

            await _service.DeleteRoundPlayer(roundPlayerToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
