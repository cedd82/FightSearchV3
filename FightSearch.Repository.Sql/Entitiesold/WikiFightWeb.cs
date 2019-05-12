//namespace FightSearch.Repository.Sql.EntitiesOld
//{
//	using System;
//	using System.Collections.Generic;
//	using System.ComponentModel.DataAnnotations.Schema;
//	using System.IO;
//	using System.Runtime.Serialization.Formatters.Binary;

//	public class WikiFightWeb
//	{
//		public DateTime DateHeld { get; set; }

//		public int EventId { get; set; }

//		public string EventName { get; set; }

//		public string Fighter1Name { get; set; }

//		public string Fighter2Name { get; set; }

//		public int? FightId { get; set; }

//		public string FightResult { get; set; }

//		public string FightResultHow { get; set; }

//		public string FightResultSubType { get; set; }

//		public string FightResultType { get; set; }

//		public bool Fotn { get; set; }

//		[DatabaseGenerated(DatabaseGeneratedOption.None)]
//		public int Id { get; set; }

//		public string ImageForWeb { get; set; }

//		public string ImagePath { get; set; }

//		public bool Potn { get; set; }

//		public string Promotion { get; set; }

//		public int RedditTopFights { get; set; }

//		public string Round { get; set; }

//		public string Time { get; set; }

//		public bool TitleFight { get; set; }

//		public TimeSpan TotalTime { get; set; }

//		public int TotalTimeEnum { get; set; }

//		public string VideoLink { get; set; }

//		public string WeightClass { get; set; }

//		public int WikiFightId { get; set; }

//		//[ForeignKey("EventId")]
//		//public WikiEvent WikiEvent { get; set; }

//	}
//}