using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class WikiFight
    {
        public int Id { get; set; }
        [StringLength(2056)]
        public string WeightClass { get; set; }
        [StringLength(2056)]
        public string Fighter1Name { get; set; }
        [StringLength(2056)]
        public string Fighter1WikiLink { get; set; }
        public string Fighter1Pic { get; set; }
        [StringLength(2056)]
        public string Fighter2Name { get; set; }
        [StringLength(2056)]
        public string Fighter2WikiLink { get; set; }
        public string Fighter2Pic { get; set; }
        [StringLength(2056)]
        public string FightResult { get; set; }
        [StringLength(2056)]
        public string FightResultHow { get; set; }
        [StringLength(2056)]
        public string FightResultType { get; set; }
        [StringLength(2056)]
        public string FightResultSubType { get; set; }
        [StringLength(2056)]
        public string Round { get; set; }
        [StringLength(2056)]
        public string Time { get; set; }
        public TimeSpan? TotalTime { get; set; }
        [StringLength(2056)]
        public string Notes { get; set; }
        [StringLength(2056)]
        public string CardType { get; set; }
        [StringLength(2056)]
        public string LinkUpdateNote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateUpdated { get; set; }
        public int? EventId { get; set; }
        public int? FightId { get; set; }
        [StringLength(2056)]
        public string DuplicateFightIds { get; set; }
        [StringLength(2056)]
        public string ImageForWeb { get; set; }
        public int? WatchCount { get; set; }
        public int? RedditTopFights { get; set; }
        [Column("FOTN")]
        public bool? Fotn { get; set; }
        [Column("POTN")]
        public bool? Potn { get; set; }
        [Column("SOTN")]
        public bool? Sotn { get; set; }
        [Column("KOTN")]
        public bool? Kotn { get; set; }
        public bool? TitleFight { get; set; }
        [StringLength(1024)]
        public string VideoLink { get; set; }
        [StringLength(2056)]
        public string ImagePath { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("WikiFight")]
        public virtual WikiEvent Event { get; set; }
        [ForeignKey("FightId")]
        [InverseProperty("WikiFight")]
        public virtual Fight Fight { get; set; }
        [InverseProperty("WikiFight")]
        public virtual WatchCount WatchCountNavigation { get; set; }
    }
}