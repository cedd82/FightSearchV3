using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.EntitiesOld
{
    public partial class Fighter
    {
        public int Id { get; set; }
        public string FighterName { get; set; }
        public int? OldLinkCount { get; set; }
        public int? NewLinkCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        public bool? Scraped { get; set; }
    }
}