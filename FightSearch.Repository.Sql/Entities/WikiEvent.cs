namespace FightSearch.Repository.Sql.Entities
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class WikiEvent
	{
		public string Attendance { get; set; }

		public DateTime DateHeld { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id { get; set; }

		public string Link { get; set; }

		public string Location { get; set; }

		public string Name { get; set; }

		public bool Processed { get; set; }

		public string Promotion { get; set; }

		public string SequenceNo { get; set; }

		public string Venue { get; set; }

		public ICollection<WikiFight> WikiFights { get; set; }

		//public ICollection<WikiFightWeb> WikiFightWebs { get; set; }
	}
}