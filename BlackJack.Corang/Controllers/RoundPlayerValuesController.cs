using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels.RoundPlayerServiceViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlackJack.Corang.Controllers
{
    [Route("api/[controller]")]
    public class RoundPlayerValuesController : ControllerBase
    {
        private readonly RoundPlayerService _roundPlayerService;

        public RoundPlayerValuesController(RoundPlayerService roundPlayerService)
        {
            _roundPlayerService = roundPlayerService;
        }


        [HttpGet("[action]")]
        public async Task<IEnumerable<RoundPlayerServiceViewModel>> RetrieveAllRoundPlayers()
        {
            var result = await _roundPlayerService.RetrieveAllRoundPlayers();
            return result;
        }

        [HttpGet("[action]/{id}")]
        public async Task<RoundPlayerServiceViewModel> RetrieveRoundPlayer(string id)
        {
            var result = await _roundPlayerService.RetrieveRoundPlayer(id);
            return result;
        }

        [HttpPost("[action]")]
        public async Task<string> CreateRoundPlayer([FromBody]RoundPlayerServiceCreateRoundPlayerViewModel viewModel)
        {
            var result = await _roundPlayerService.CreateRoundPlayer(viewModel);
            return result;
        }

        [HttpPut("[action]/{id}")]
        public async Task<bool> UpdateRoundPlayer(string id, [FromBody]RoundPlayerServiceViewModel viewModel)
        {
            if (id == viewModel.Id)
            {
                var isUpdated = await _roundPlayerService.UpdateRoundPlayer(viewModel);
                return isUpdated;
            }
            return false;
        }

        [HttpDelete("[action]/{id}")]
        public async Task<int> DeleteRoundPlayer(string id)
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
