using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository;

public class OddRepository : IOddRepository
{
    protected readonly ITryBetsContext _context;
    public OddRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public Match Patch(int MatchId, int TeamId, string BetValue)
    {
        string betValueConverted = BetValue.Replace(',', '.');
        decimal decimalBetValue = decimal.Parse(betValueConverted, CultureInfo.InvariantCulture);
        
        var match = _context.Matches.First(m => m.MatchId == MatchId);

        if (match.MatchTeamAId != TeamId && match.MatchTeamBId != TeamId)
        {
            throw new System.Exception("Team is not in this match");
        }
        
        if (match.MatchTeamAId == TeamId) match.MatchTeamAValue += decimalBetValue;
        else match.MatchTeamBValue += decimalBetValue;

        return match;
    }
}