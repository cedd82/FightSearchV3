using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class WatchCount
    {
        public int WikiFightId { get; set; }
        public int Count { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdateDateTime { get; set; }

        [ForeignKey("WikiFightId")]
        [InverseProperty("WatchCountNavigation")]
        public virtual WikiFight WikiFight { get; set; }
    }
}