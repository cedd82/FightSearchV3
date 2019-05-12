//namespace FightSearch.Repository.Sql.EntitiesOld
//{
//	using System;
//	using System.ComponentModel.DataAnnotations;
//	using System.ComponentModel.DataAnnotations.Schema;

//	public class Fight
//	{
//		public bool? BrokenLink { get; set; }

//		public DateTime? Date { get; set; }

//		public DateTime? DateProcessed { get; set; }

//		public string Description { get; set; }

//		public int EventId { get; set; }

//		public string Fighter1Name { get; set; }

//		public string Fighter2Name { get; set; }

//		public bool FromBellatorLibrary { get; set; }

//		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//		[Key]
//		public int Id { get; set; }

//		public bool IgnoreFight { get; set; }

//		public string ImagePath { get; set; }

//		public string ImageUrl { get; set; }

//		public DateTime InsertDateTime { get; set; }

//		public bool IsDuplicateOfAnotherFight { get; set; }

//		public string Name { get; set; }

//		public string NameFixed { get; set; }

//		public string Page { get; set; }

//		public bool Prelim { get; set; }

//		public bool Processed { get; set; }

//		public string Promotion { get; set; }

//		public int SequenceNo { get; set; }

//		public DateTime? UpdateDateTime { get; set; }

//		public string UpdateNote { get; set; }

//		public string VideoLink { get; set; }

//		public int? WasIgnoredNowNewFightId { get; set; }

//		//public WikiFight WikiFight { get;set; }
		
//	}
//}