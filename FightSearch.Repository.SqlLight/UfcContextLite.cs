using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FightSearch.Repository.SqlLight
{
    public class UfcContextLite : DbContext
    {
        public UfcContextLite(DbContextOptions<UfcContextLite> options)
            : base(options)
        { }

        public UfcContextLite()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Set the filename of the database to be created
            //optionsBuilder.UseSqlite("Data Source=db.UfcSqlite");

            optionsBuilder.UseSqlite("Filename=UfcSqlite.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });


            base.OnConfiguring(optionsBuilder);
        }

        //public virtual DbSet<Fight> Fight { get; set; }
        //public virtual DbSet<Fighter> Fighter { get; set; }
        //public virtual DbSet<FighterNickName> FighterNickName { get; set; }
        //public virtual DbSet<NameChange> NameChange { get; set; }
        //public virtual DbSet<SearchTermHistory> SearchTermHistory { get; set; }
        //public virtual DbSet<Visitor> Visitor { get; set; }
        //public virtual DbSet<WatchCount> WatchCount { get; set; }
        //public virtual DbSet<WikiEvent> WikiEvent { get; set; }
        //public virtual DbSet<WikiFight> WikiFight { get; set; }
        public virtual DbSet<WikiFightWebSqlLite> WikiFightWebSqlLite { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WikiFightWebSqlLite>().ToTable("WikiFightWeb", "main");
            modelBuilder.Entity<WikiFightWebSqlLite>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ImageForWeb).IsUnicode(false);
                entity.Property(e => e.ImagePath).IsUnicode(false);
                entity.Property(e => e.WeightClass).IsUnicode(false);
                entity.Property(e => e.Fighter1Name).IsUnicode(false);
                entity.Property(e => e.Fighter2Name).IsUnicode(false);
                entity.Property(e => e.FightResult).IsUnicode(false);
                entity.Property(e => e.FightResult).IsUnicode(false);
                entity.Property(e => e.FightResultHow).IsUnicode(false);
                entity.Property(e => e.FightResultType).IsUnicode(false);
                entity.Property(e => e.FightResultSubType).IsUnicode(false);

                //entity.HasOne(d => d.Event)
                //    .WithMany(p => p.WikifightWeb)
                //    .HasForeignKey(d => d.EventId)
                //    .HasConstraintName("FK_wikifightWeb_WikiEvent");
            });
        }
    }
}
