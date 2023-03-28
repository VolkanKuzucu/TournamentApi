using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TournamentApi.DataAccess
{
    public interface IMatchRepository
    {
        Task<List<Match>> CreateFixture();
        Task ClearFixture();
        Task<List<Match>> PlayTheMatchesOfTheWeek();
    }
}