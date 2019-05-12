using System.Threading.Tasks;
using FightSearch.Repository.Sql.Entities;
using Microsoft.EntityFrameworkCore;

namespace FightSearch.Repository.Sql
{
	public interface IFightSearchEntities
	{
		DbSet<Fight> Fight { get; }
		DbSet<WikiEvent> WikiEvent { get; }
		DbSet<WikiFight> WikiFight { get; }
		DbSet<WikiFightWeb> WikiFightWeb { get; }
		void Commit();
		Task CommitAsync();
		//DbSet<T> Set<T>() where T : class;
	}
}