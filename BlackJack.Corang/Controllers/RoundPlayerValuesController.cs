using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class RoundPlayerValuesController : ControllerBase
    {
        private readonly RoundPlayerService _roundPlayerService;

        public RoundPlayerValuesController(RoundPlayerService roundPlayerService)
        {
            _roundPlayerService = roundPlayerService;
        }


        [HttpGet]
        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveAllRounds()
        {
            var result = await _roundPlayerService.RetrieveAllRoundPlayers();
            return result;
        }

        [HttpGet]
        public async Task<RoundPlayerServiceViewModel> RetrieveRound(string id)
        {
            var result = await _roundPlayerService.RetrieveRoundPlayer(id);
            return result;
        }

        [HttpPost]
        public async Task<string> CreateRound([FromBody]RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            var result = await _roundPlayerService.CreateRoundPlayer(viewModel);
            return result;
        }

        [HttpPut]
        public async Task<bool> UpdateRound(string id, [FromBody]RoundPlayerServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _roundPlayerService.UpdateRoundPlayer(viewModel);
                return isUpdated;
            }
            return false;
        }


        public async Task<int> DeleteRound(string id)
        {

            var roundPlayerToDelete = await _roundPlayerService.RetrieveRoundPlayer(id);
            if (roundPlayerToDelete.Id != Guid.Empty.ToString())
            {
                var result = await _roundPlayerService.DeleteRoundPlayer(roundPlayerToDelete);
                return result;
            }
            return 0;
        }

    }
}
