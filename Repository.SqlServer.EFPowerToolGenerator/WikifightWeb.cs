using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class WikiFightWeb
    {
        public int Id { get; set; }
        public int WikiFightId { get; set; }
        [Required]
        [StringLength(2056)]
        public string WeightClass { get; set; }
        [Required]
        [StringLength(2056)]
        public string Fighter1Name { get; set; }
        [Required]
        [StringLength(2056)]
        public string Fighter2Name { get; set; }
        [Required]
        [StringLength(2056)]
        public string FightResult { get; set; }
        [Required]
        [StringLength(2056)]
        public string FightResultHow { get; set; }
        [Required]
        [StringLength(2056)]
        public string FightResultType { get; set; }
        [Required]
        [StringLength(2056)]
        public string FightResultSubType { get; set; }
        [Required]
        [StringLength(2056)]
        public string Round { get; set; }
        [Required]
        [StringLength(2056)]
        public string Time { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int TotalTimeEnum { get; set; }
        public int EventId { get; set; }
        public int FightId { get; set; }
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
        public DateTime DateHeld { get; set; }
        [Required]
        [StringLength(2056)]
        public string EventName { get; set; }
        [Required]
        [StringLength(256)]
        public string Promotion { get; set; }

        [ForeignKey("EventId")]
        [InverseProperty("WikiFightWeb")]
        public virtual WikiEvent Event { get; set; }
    }
}