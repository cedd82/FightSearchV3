using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class Fight
    {
        public Fight()
        {
            WikiFight = new HashSet<WikiFight>();
        }

        public int Id { get; set; }
        public int? SequenceNo { get; set; }
        [StringLength(1024)]
        public string VideoLink { get; set; }
        public string ImagePath { get; set; }
        [StringLength(1024)]
        public string ImageUrl { get; set; }
        [StringLength(1024)]
        public string Fighter1Name { get; set; }
        [StringLength(1024)]
        public string Fighter2Name { get; set; }
        [StringLength(1024)]
        public string NameFixed { get; set; }
        [StringLength(1024)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        public int? EventId { get; set; }
        public bool? Prelim { get; set; }
        [StringLength(1024)]
        public string Promotion { get; set; }
        public bool? Processed { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateProcessed { get; set; }
        [StringLength(1024)]
        public string Page { get; set; }
        [StringLength(4000)]
        public string UpdateNote { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDateTime { get; set; }
        public bool? IsDuplicateOfAnotherFight { get; set; }
        public bool? IgnoreFight { get; set; }
        public int? WasIgnoredNowNewFightId { get; set; }
        public bool? FromScrapingSerach { get; set; }
        public bool? FromBellatorLibrary { get; set; }
        public bool? BrokenLink { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime InsertDateTime { get; set; }

        [InverseProperty("Fight")]
        public virtual ICollection<WikiFight> WikiFight { get; set; }
    }
}