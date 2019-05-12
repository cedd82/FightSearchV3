using System.ComponentModel;

namespace FightSearch.Common.Enums
{
    public enum FinishTypes
    {
	    [Description("Any")] Any = 1,
	    [Description("KO")] KO = 2,
	    [Description("Submission")] Submission = 3,
	    [Description("Decision Unanimous")] DecisionUnanimous = 4,
	    [Description("Decision")] Decision = 5,
	    [Description("Others")] Others = 6,
	    [Description("TKO")] TKO = 7
	};
}
