using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class SearchTermHistory
    {
        public int Id { get; set; }
        public string SearchTerm1 { get; set; }
        public string SearchTerm2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDateTime { get; set; }
        public int? TimesUsedCount { get; set; }
        public int? LastYieldCount { get; set; }
    }
}