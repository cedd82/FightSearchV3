using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.Entities
{
    public partial class NameChange
    {
        public int Id { get; set; }
        [StringLength(512)]
        public string FromName { get; set; }
        [StringLength(512)]
        public string ToName { get; set; }
        public int? FighterNoChanged { get; set; }
        public string FightId { get; set; }
        public bool? IsNickname { get; set; }
        public bool? Ignore { get; set; }
        public string WikifightIdsUpdated { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ManualFixDate { get; set; }
    }
}