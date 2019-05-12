namespace FightSearch.Common.Enums
{
	using System.ComponentModel;

	public enum DecisionFinishTypes
	{
		[Description("Decision Majority")] DecisionMajority,
		[Description("DecisionSplit")] DecisionSplit,
		[Description("Draw")] Draw,
		[Description("No Contest")] NoContest,
		[Description("Disqualification")] Disqualification
	}
}