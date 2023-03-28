using System.Threading.Tasks;

namespace TournamentApi.DataAccess
{
    public interface ITeamRepository
    {
        Task UpdateTeamBoard(int matchId, int homeTeamScore, int awayTeamScore);
    }
}