using BlackJack.BusinessLogic.GameLogic;
using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.RoundServiceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.Presentation.Controllers
{
    public class RoundValuesController : ApiController
    {
        private readonly RoundService _service;

        public RoundValuesController(RoundService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IEnumerable<RoundServiceViewModel>> RetrieveAllRounds()
        {
            var result = await _service.RetrieveAllRounds();
            return result;
        }

        [HttpGet]
        public async Task<RoundServiceViewModel> RetrieveRound(string id)
        {
            var result = await _service.RetrieveRound(id);
            return result;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> CreateRound([FromBody]RoundServiceCreateRoundViewModel viewModel)
        {
            viewModel.Deck = (new DeckLogic()).Stringify();
            var result = await _service.CreateRound(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateRound(string id, [FromBody]RoundServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _service.UpdateRound(viewModel);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.NotModified);
        }


        public async Task<HttpResponseMessage> DeleteRound(string id)
        {
            var roundToDelete = await _service.RetrieveRound(id);

            await _service.DeleteRound(roundToDelete);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
