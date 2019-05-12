using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.EntitiesOld
{
    [Table("wikifightWeb")]
    public partial class WikifightWeb
    {
        public int Id { get; set; }
        public int? WikiFightId { get; set; }
        [StringLength(2056)]
        public string WeightClass { get; set; }
        [StringLength(2056)]
        public string Fighter1Name { get; set; }
        [StringLength(2056)]
        public string Fighter2Name { get; set; }
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
        public int? TotalTimeEnum { get; set; }
        public int? EventId { get; set; }
        public int? FightId { get; set; }
        [StringLength(2056)]
        public string ImageForWeb { get; set; }
        public int RedditTopFights { get; set; }
        [Column("FOTN")]
        public bool Fotn { get; set; }
        [Column("POTN")]
        public bool Potn { get; set; }
        public bool TitleFight { get; set; }
        [StringLength(1024)]
        public string VideoLink { get; set; }
        [StringLength(2056)]
        public string ImagePath { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateHeld { get; set; }
        [StringLength(2056)]
        public string EventName { get; set; }
        [StringLength(256)]
        public string Promotion { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("WikifightWeb")]
        public virtual WikiEvent Event { get; set; }
    }
}