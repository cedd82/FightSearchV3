using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.EntitiesOld
{
    public partial class WikiEvent
    {
        public WikiEvent()
        {
            Wikifight = new HashSet<Wikifight>();
            WikifightWeb = new HashSet<WikifightWeb>();
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
        public virtual ICollection<Wikifight> Wikifight { get; set; }
        [InverseProperty("Event")]
        public virtual ICollection<WikifightWeb> WikifightWeb { get; set; }
    }
}