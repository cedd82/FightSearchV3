using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FightSearch.Repository.SqlLight
{
    //[Table("WikiFightWeb")]
    public partial class WikiFightWebSqlLite
    {
        public int Id { get; set; }
        public int WikiFightId { get; set; }
        //[StringLength(2056)]
        
        public string WeightClass { get; set; }
        
        
        public string Fighter1Name { get; set; }
        ////[Required]
        //[StringLength(2056)]
        public string Fighter2Name { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string FightResult { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string FightResultHow { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string FightResultType { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string FightResultSubType { get; set; }
        //[Required]
        //[StringLength(2056)]
        public int Round { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string Time { get; set; }
        public string TotalTime { get; set; }
        public int TotalTimeEnum { get; set; }
        public int EventId { get; set; }
        public int FightId { get; set; }
        //[StringLength(2056)]
        public string ImageForWeb { get; set; }
        public int RedditTopFights { get; set; }
        [Column("FOTN")]
        public int Fotn { get; set; }
        [Column("POTN")]
        public int Potn { get; set; }
        public int TitleFight { get; set; }
        //[StringLength(1024)]
        public string VideoLink { get; set; }
        //[StringLength(2056)]
        public string ImagePath { get; set; }
        //[Column(TypeName = "datetime")]
        public string DateHeld { get; set; }
        //[Required]
        //[StringLength(2056)]
        public string EventName { get; set; }
        //[Required]
        //[StringLength(256)]
        public string Promotion { get; set; }

        //[ForeignKey("EventId")]
        //[InverseProperty("WikiFightWeb")]
        //public virtual WikiEvent Event { get; set; }
    }
}