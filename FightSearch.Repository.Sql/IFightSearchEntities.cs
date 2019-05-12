namespace FightSearch.Repository.Sql
{
    using System.Threading;
    using System.Threading.Tasks;

    using FightSearch.Repository.Sql.Entities;

    using Microsoft.EntityFrameworkCore;

    public interface IFightSearchEntities
    {
        DbSet<Fight> Fight { get; }
        DbSet<WikiEvent> WikiEvent { get; }
        DbSet<WikiFight> WikiFight { get; }
        DbSet<WikiFightWeb> WikiFightWeb { get; }

        DbSet<Fighter> Fighter { get; }
        DbSet<FighterNickName> FighterNickName { get; }
        DbSet<NameChange> NameChange { get; }
        DbSet<SearchTermHistory> SearchTermHistory { get; }
        DbSet<Visitor> Visitor { get; }
        DbSet<WatchCount> WatchCount { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}