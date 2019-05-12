using System.ComponentModel;

namespace FightSearch.Common.Enums
{
    public enum Durations
    {
		[Description("Any")] Any = 0,
	    [Description("Five")] Five = 1,
	    [Description("Ten")] Ten = 2,
	    [Description("FifteenOrLess")] FifteenOrLess = 3,
		[Description("Fifteen")] Fifteen = 4,
	    [Description("FifteenPlus")] FifteenPlus = 5
    }
}
