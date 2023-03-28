using System.Threading.Tasks;

namespace TournamentApi.DataAccess
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TournamentContext _tournamentContext;

        public TeamRepository(TournamentContext tournamentContext)
        {
            _tournamentContext = tournamentContext;
        }

        public async Task UpdateTeamBoard(int matchId, int homeTeamScore, int awayTeamScore)
        {
            // var updateTeam = await _tournamentContext.Team.Where(x => x.Id== match.LooserTeam.Id || x.Id == match.WinnerTeam.Id).ToListAsync();
            //
            // foreach (var team in updateTeam)
            // {
            //     if (match.IsDraw)
            //     {
            //         team.Draws += 1;
            //         team.GoalAgainst = match.TotalGoals / 2;
            //         team.GoalFor = match.TotalGoals / 2;
            //         team.Points += 1;
            //     }
            //     else
            //     {
            //         if (team.Id == match.WinnerTeam.Id)
            //         {
            //             team.Wins += 1;
            //             team.GoalFor = match.WinnerTeam.GoalFor;
            //             team.GoalFor = match
            //         }
            //     }
            // }

            var match = await _tournamentContext.Match.FindAsync(matchId);

            var homeTeam = await _tournamentContext.Team.FindAsync(match.HomeTeamId);
            var awayTeam = await _tournamentContext.Team.FindAsync(match.AwayTeamId);

            if (homeTeamScore > awayTeamScore)
            {
                homeTeam.Points += 3;
                homeTeam.Wins += 1;
                awayTeam.Losses += 1;
            }
            else if (homeTeamScore < awayTeamScore)
            {
                awayTeam.Points += 3;
                awayTeam.Wins += 1;
                homeTeam.Losses += 1;
            }
            else
            {
                homeTeam.Points += 1;
                awayTeam.Points += 1;
                homeTeam.Draws += 1;
                awayTeam.Draws += 1;
            }

            homeTeam.GoalFor += homeTeamScore;
            homeTeam.GoalAgainst += awayTeamScore;
            homeTeam.GoalDifference = homeTeam.GoalFor - homeTeam.GoalAgainst;

            awayTeam.GoalFor += awayTeamScore;
            awayTeam.GoalAgainst += homeTeamScore;
            awayTeam.GoalDifference = awayTeam.GoalFor - awayTeam.GoalAgainst;

            match.HomeTeamScore = homeTeamScore;
            match.AwayTeamScore = awayTeamScore;
        }
    }
}