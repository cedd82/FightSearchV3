using System.ComponentModel.DataAnnotations;
using FightSearch.Common.Enums;

namespace FightSearch.Service.DomainModels
{
	public class SearchParams
	{
		[EnumDataType(typeof(AwardTypes))]
		public AwardTypes AwardType { get; set; }
		public Durations Duration { get; set; }
		public string Fighter1Name { get; set; }
		public FinishTypes FinishType { get; set; }
		public bool FreeFightsOnly { get; set; }
		public string KoType { get; set; }
		public bool? Sfn { get; set; }
		public string SubmissionType { get; set; }
		public WeightDivisions WeightDivision { get; set; }
		public string Year { get; set; }
	}
}