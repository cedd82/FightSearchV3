namespace FightSearch.Repository.Sql.Entities
{
	using System;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class WikiFight
	{
		public string CardType { get; set; }

		public DateTime? DateUpdated { get; set; }

		public string DuplicateFightIds { get; set; }
		
		public string Fighter1Name { get; set; }

		public string Fighter1Pic { get; set; }

		public string Fighter1WikiLink { get; set; }

		public string Fighter2Name { get; set; }

		public string Fighter2Pic { get; set; }

		public string Fighter2WikiLink { get; set; }

		public int? FightId { get; set; }

		public string FightResult { get; set; }

		public string FightResultHow { get; set; }

		public string FightResultSubType { get; set; }

		public string FightResultType { get; set; }

		public bool? FOTN { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		public string ImageForWeb { get; set; }

		public string ImagePath { get; set; }

		public bool? KOTN { get; set; }

		public string LinkUpdateNote { get; set; }

		public string Notes { get; set; }

		public bool? POTN { get; set; }

		public int? RedditTopFights { get; set; }

		public string Round { get; set; }

		public bool? SOTN { get; set; }

		public string Time { get; set; }

		public bool? TitleFight { get; set; }

		public TimeSpan TotalTime { get; set; }

		public string VideoLink { get; set; }

		public int? WatchCount { get; set; }

		public string WeightClass { get; set; }
		
		public int? EventId { get; set; }

		[ForeignKey("EventId")]
		public WikiEvent WikiEvent { get; set; }

		[ForeignKey("FightId")]
		public Fight Fight { get; set; }
	}
}