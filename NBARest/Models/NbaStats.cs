using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace NBARest.Models
{
    public class NbaStats
    {
        [Key]
        public int playerId { get; set; }
        public string seasonId { get; set; }
        public string leagueId { get; set; }
        public int teamId { get; set; }
        public string teamAbrv { get; set; }
        public int playerAge { get; set; }
        public double gamesPlayed { get; set; }
        public double gamesStarted { get; set; }
        public double minutes { get; set; }
        public double fga { get; set; }
        public double fgm { get; set; }
        public double fgPercent { get; set; }
        public double fg3m { get; set; }
        public double fg3a { get; set; }
        public double fg3Percent { get; set; }
        public double ftm { get; set; }
        public double fta { get; set; }
        public double ftPercent { get; set; }
        public double oReb { get; set; }
        public double dReb { get; set; }
        public double totReb { get; set; }
        public double ast { get; set; }
        public double stl { get; set; }
        public double blk { get; set; }
        public double tov { get; set; }
        public double pf { get; set; }
        public double pts { get; set; }


        public string printStats()
        {
            return ast.ToString();
        }

  
    }

    public class NbaDBContext : DbContext
    {
        public DbSet<NbaStats> Movies { get; set; }
    }
}
