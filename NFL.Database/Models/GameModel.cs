using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFL.Database.Models
{
    public class GameModel : BaseModel
    {
        public int Id { get; set; }

        public int Week { get; set; }

        public DateTime StartTime { get; set; }

        [Column(TypeName = "decimal(2,1)")]
        public decimal? Spread { get; set; } // based on home team

        public int? AwaySpreadOdds { get; set; }

        public int? HomeSpreadOdds { get; set; }

        public int? AwayMoneyLine { get; set; }

        public int? HomeMoneyLine { get; set; }

        [Column(TypeName = "decimal(2,1)")]
        public decimal? OverUnder { get; set; }

        public int? OverOdds { get; set; }

        public int? UnderOdds { get; set; }

        public bool IsPlayoffs { get; set; }

        public bool WasOvertime { get; set; }

        public int? Bet { get; set; }

        public int AwayTeamId { get; set; }

        public int AwayTeamScore { get; set; }

        public int HomeTeamId { get; set; }

        public int HomeTeamScore { get; set; }

        public int SeasonId { get; set; }

        public int UserId { get; set; }

        public virtual TeamModel AwayTeam { get; set; }

        public virtual TeamModel HomeTeam { get; set; }

        public virtual SeasonModel Season { get; set; }

        public virtual UserModel User { get; set; }


        /// <summary>
        /// Update the Games table in the database with the model passed in 
        /// </summary>
        /// <param name="model">GameModel to update in the database</param>
        /// <exception cref="NullReferenceException">If model is null</exception>
        /// <exception cref="InvalidOperationException">If new Game Id doesn't match actual model in database</exception>
        internal void UpdateModel(GameModel model)
        {
            // validate
            if (model == null) throw new NullReferenceException("input model cannot be null");
            if (model.Id != Id) throw new InvalidOperationException("Id mismatch: incoming model and current model are different");

            // update
            if (IsUpdateRequired(Week, model.Week)) Week = model.Week;
            if (IsUpdateRequired(StartTime, model.StartTime)) StartTime = model.StartTime;
            if (IsUpdateRequired(Spread, model.Spread)) Spread = model.Spread;
            if (IsUpdateRequired(AwaySpreadOdds, model.AwaySpreadOdds)) AwaySpreadOdds = model.AwaySpreadOdds;
            if (IsUpdateRequired(HomeSpreadOdds, model.HomeSpreadOdds)) HomeSpreadOdds = model.HomeSpreadOdds;
            if (IsUpdateRequired(AwayMoneyLine, model.AwayMoneyLine)) AwayMoneyLine = model.AwayMoneyLine;
            if (IsUpdateRequired(OverUnder, model.OverUnder)) OverUnder = model.OverUnder;
            if (IsUpdateRequired(OverOdds, model.OverOdds)) OverOdds = model.OverOdds;
            if (IsUpdateRequired(UnderOdds, model.UnderOdds)) UnderOdds = model.UnderOdds;
            if (IsUpdateRequired(IsPlayoffs, model.IsPlayoffs)) IsPlayoffs = model.IsPlayoffs;
            if (IsUpdateRequired(WasOvertime, model.WasOvertime)) WasOvertime = model.WasOvertime;
            if (IsUpdateRequired(Bet, model.Bet)) Bet = model.Bet;
            if (IsUpdateRequired(AwayTeamScore, model.AwayTeamScore)) AwayTeamScore = model.AwayTeamScore;
            if (IsUpdateRequired(HomeTeamScore, model.HomeTeamScore)) HomeTeamScore = model.HomeTeamScore;
        }
    }
}
