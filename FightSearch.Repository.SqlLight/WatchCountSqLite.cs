using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSearch.Repository.SqlLight
{
    [Table("WatchCount")]
    public class WatchCountSqLite
    {
        public int WikiFightId { get; set; }
        public int Count { get; set; }
        public string UpdateDateTime { get; set; }

    }
}
