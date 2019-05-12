//using System.Threading.Tasks;
////using FightSearch.Repository.Sql.EntitiesOld;
//using Microsoft.EntityFrameworkCore;

//namespace FightSearch.Repository.Sql
//{
//    using FightSearch.Repository.Sql.Entities;
    
//    //using FightSearch.Repository.Sql.EntitiesOld;

//    using Fight = FightSearch.Repository.Sql.Entities.Fight;
//    using WikiEvent = FightSearch.Repository.Sql.Entities.WikiEvent;

//    public class FightSearchEntities : DbContext, IFightSearchEntities
//	{
//		public FightSearchEntities(DbContextOptions<FightSearchEntities> options) : base(options)
//		{
//		}

//		public void Commit()
//		{
//			SaveChanges();
//		}

//		public async Task CommitAsync()
//		{
//			await SaveChangesAsync();
//		}
//        public virtual DbSet<Fight> Fight { get; set; }
//        public virtual DbSet<FightPassVideoScrape> FightPassVideoScrape { get; set; }
//        public virtual DbSet<NameChange> NameChange { get; set; }
//        public virtual DbSet<SearchTermHistory> SearchTermHistory { get; set; }
//        public virtual DbSet<Visitor> Visitor { get; set; }
//        public virtual DbSet<WatchCount> WatchCount { get; set; }
//        public DbSet<WikiEvent> WikiEvent { get; set; }
//		public DbSet<WikiFight> WikiFight { get; set; }
//		public DbSet<WikiFightWeb> WikiFightWeb { get; set; }

//		protected override void OnModelCreating(ModelBuilder modelBuilder)
//		{
//		}

//		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//		{
			
//		}
//	}
//}