using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.EntitiesOld
{
    public partial class Visitor
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string VisitorIp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDateTime { get; set; }
        public int? Count { get; set; }
    }
}