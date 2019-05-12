using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FightSearch.Repository.Sql.Entities
{
    public partial class FighterNickName
    {
        public int Id { get; set; }
        [StringLength(1024)]
        public string FighterName { get; set; }
        [StringLength(1024)]
        public string NickName { get; set; }
    }
}