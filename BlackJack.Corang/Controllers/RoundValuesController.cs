using BlackJack.BusinessLogic.GameLogic;
using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.RoundServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlackJack.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class RoundValuesController : ControllerBase
    {
        private readonly RoundService _roundService;

        public RoundValuesController(RoundService roundService)
        {
            _roundService = roundService;
        }


        [HttpGet]
        public async Task<IEnumerable<RoundServiceViewModel>> RetrieveAllRounds()
        {
            var result = await _roundService.RetrieveAllRounds();
            return result;
        }

        [HttpGet]
        public async Task<RoundServiceViewModel> RetrieveRound(string id)
        {
            var result = await _roundService.RetrieveRound(id);
            return result;
        }

        [HttpPost]
        public async Task<string> CreateRound([FromBody]RoundServiceCreateRoundViewModel viewModel)
        {
            viewModel.Deck = (new DeckLogic()).Stringify();
            var result = await _roundService.CreateRound(viewModel);
            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateRound(string id, [FromBody]RoundServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _roundService.UpdateRound(viewModel);
                return isUpdated;
            }
            return false;
        }


        public async Task<int> DeleteRound(string id)
        {
            var roundToDelete = await _roundService.RetrieveRound(id);
            if (roundToDelete.Id != Guid.Empty.ToString())
            {
                var result = await _roundService.DeleteRound(roundToDelete);
                return result;
            }
            return 0;
        }
    }
}
