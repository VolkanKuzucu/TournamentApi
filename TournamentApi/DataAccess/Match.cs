using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.SymbolStore;

namespace TournamentApi.DataAccess
{
    public class Match
    {
        [Key]
        public int Id { get; set; } // primary key
        public int HomeTeamId { get; set; } // foreign key
        public int AwayTeamId { get; set; } // foreign key
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public int Week { get; set; }
        [DefaultValue(false)]
        public bool IsPlayed { get; set; }
    }

    public class PlayedMatchDto
    {
        public Team WinnerTeam{ get; set; }
        public Team LooserTeam{ get; set; }
        public bool IsDraw { get; set; }

        public int TotalGoals { get; set; }
    }
}