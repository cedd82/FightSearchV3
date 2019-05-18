using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.SqlServer.EFPowerToolGenerator
{
    public partial class WikiEvent
    {
        public WikiEvent()
        {
            WikiFight = new HashSet<WikiFight>();
            WikiFightWeb = new HashSet<WikiFightWeb>();
        }

        public int Id { get; set; }
        [StringLength(256)]
        public string SequenceNo { get; set; }
        [StringLength(2056)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateHeld { get; set; }
        [StringLength(2056)]
        public string Venue { get; set; }
        [StringLength(2056)]
        public string Location { get; set; }
        [StringLength(256)]
        public string Attendance { get; set; }
        [StringLength(256)]
        public string Promotion { get; set; }
        [StringLength(2056)]
        public string Link { get; set; }
        public bool? Processed { get; set; }

        [InverseProperty("Event")]
        public virtual ICollection<WikiFight> WikiFight { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<WikiFightWeb> WikiFightWeb { get; set; }
    }
}