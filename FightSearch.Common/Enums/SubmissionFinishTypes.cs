namespace FightSearch.Common.Enums
{
	using System.ComponentModel;

	public enum SubmissionFinishTypes
	{
		[Description("Any")] Any,
		[Description("Arm Triangle Choke")] ArmTriangleChoke,
		[Description("Armbar")] Armbar,
		[Description("Guillotine Choke")] GuillotineChoke,
		[Description("Injury")] Injury,
		[Description("Leg Lock")] LegLock,
		[Description("Other Chokes")] OtherChokes,
		[Description("Others")] Others,
 		[Description("Rear Naked Choke")] RearNakedChoke,
		[Description("Shoulder Lock")] ShoulderLock,
		[Description("Strikes")] Strikes,
		[Description("Triangle Choke")] TriangleChoke
	}
}