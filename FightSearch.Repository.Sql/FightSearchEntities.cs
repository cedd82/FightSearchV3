using System.Threading.Tasks;
using FightSearch.Repository.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace FightSearch.Repository.Sql
{
	public class FightSearchEntities : DbContext, IFightSearchEntities
	{
		public FightSearchEntities(DbContextOptions<FightSearchEntities> options) : base(options)
		{
		}

		public void Commit()
		{
			SaveChanges();
		}

		public async Task CommitAsync()
		{
			await SaveChangesAsync();
		}

		//DbSet<T> IFightSearchEntities.Set<T>()
		//{
		//	return Set<T>();
		//}
		public DbSet<Fight> Fight { get; set; }
		public DbSet<WikiEvent> WikiEvent { get; set; }
		public DbSet<WikiFight> WikiFight { get; set; }
		public DbSet<WikiFightWeb> WikiFightWeb { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			
		}
	}
}