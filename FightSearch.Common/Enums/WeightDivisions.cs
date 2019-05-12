using System.ComponentModel;

namespace FightSearch.Common.Enums
{
    public enum WeightDivisions
    {
        [Description("Any")] Any = 1,
        [Description("Flyweight")] Flyweight = 2,
        [Description("Bantamweight")] Bantamweight = 3,
        [Description("Featherweight")] Featherweight = 4,
        [Description("Lightweight")] Lightweight = 5,
        [Description("Welterweight")] Welterweight = 6,
        [Description("Middleweight")] Middleweight = 7,
        [Description("Light Heavyweight")] LightHeavyweight = 8,
        [Description("Heavyweight")] Heavyweight = 9,
        [Description("Super Heavyweight")] SuperHeavyweight = 10,
        [Description("Women's Flyweight")] WomensFlyweight = 11,
        [Description("Women's Strawweight")] WomensStrawweight = 12,
        [Description("Women's Bantamweight")] WomensBantamweight = 13,
        [Description("Women's Featherweight")] WomensFeatherweight = 14
    }
}