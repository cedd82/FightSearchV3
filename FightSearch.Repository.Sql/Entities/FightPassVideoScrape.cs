using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.Entities
{
    public partial class FightPassVideoScrape
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string SearchTerm1 { get; set; }
        public string SearchTerm2 { get; set; }
        public int? VideoId { get; set; }
        public int? CountOfDuplicate { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string NameFixed { get; set; }
        public string Fighter1Name { get; set; }
        public string Fighter2Name { get; set; }
        public string ImageLink { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        public bool? CanIgnore { get; set; }
        public bool? AlreadyUsed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDateTime { get; set; }
        public bool? ProcessedNames { get; set; }
        public bool? CopiedOver { get; set; }
        public bool? ActuallyIgnore { get; set; }
        public bool TitleFight { get; set; }
        [Column("FOTN")]
        public bool Fotn { get; set; }
        [Column("KOTN")]
        public bool Kotn { get; set; }
        [Column("POTN")]
        public bool Potn { get; set; }
        [Column("SOTN")]
        public bool Sotn { get; set; }
    }
}