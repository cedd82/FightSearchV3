namespace FightSearch.Common.Enums
{
	using System.ComponentModel;

	public enum KoFinishTypes
	{
		[Description("Any")] Any,
		[Description("Elbows")] Elbows,
		[Description("Flying Knee")] FlyingKnee,
		[Description("Head Kick")] HeadKick,
		[Description("Injury Or Stoppage")] InjuryOrStoppage,
		[Description("Kicks")] Kicks,
		[Description("Knees")] Knees,
		[Description("Others")] Others,
		[Description("Punches")] Punches,
		[Description("Spinning Strikes")] SpinningStrikes
	}
}