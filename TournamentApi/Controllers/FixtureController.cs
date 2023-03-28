using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentApi.DataAccess;

namespace TournamentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FixtureController : ControllerBase
    {
        private readonly IMatchRepository _matchRepository;

        public FixtureController(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<List<Match>>> Create()
        {
            await _matchRepository.ClearFixture();
            List<Match> matches = await _matchRepository.CreateFixture();
            return Ok(matches);
        }
        
        [HttpPost("PlayTheMatchesOfTheWeek")]
        public async Task<ActionResult<List<Match>>> PlayTheMatchesOfTheWeek()
        {
            try
            {
                var playTheMatchesOfTheWeek = await _matchRepository.PlayTheMatchesOfTheWeek();
                return Ok(playTheMatchesOfTheWeek);
            }
            catch (Exception exception)
            {
                return NotFound(exception.Message);
            }
        }
    }
}