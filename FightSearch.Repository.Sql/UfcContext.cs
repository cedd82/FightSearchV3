﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FightSearch.Repository.Sql
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FightSearch.Repository.Sql.Entities;

    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public partial class UfcContext : DbContext, IFightSearchEntities
    {
        public UfcContext()
        {
        }

        public UfcContext(DbContextOptions<UfcContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fight> Fight { get; set; }
        public virtual DbSet<Fighter> Fighter { get; set; }
        public virtual DbSet<FighterNickName> FighterNickName { get; set; }
        public virtual DbSet<NameChange> NameChange { get; set; }
        public virtual DbSet<SearchTermHistory> SearchTermHistory { get; set; }
        public virtual DbSet<Visitor> Visitor { get; set; }
        public virtual DbSet<WatchCount> WatchCount { get; set; }
        public virtual DbSet<WikiEvent> WikiEvent { get; set; }
        public virtual DbSet<WikiFight> WikiFight { get; set; }
        public virtual DbSet<WikiFightWeb> WikiFightWeb { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=cpc\\MSSQLSERVERSEVEN;Initial Catalog=ufc;Persist Security Info=True;User ID=ufc;Password=blade1");
            }
        }

        public override int SaveChanges()
        {
			IEnumerable<EntityEntry> changedEntities = ChangeTracker
													   .Entries()
													   .Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified);



			List<ValidationResult> errors = new List<ValidationResult>(); // all errors are here
            foreach (EntityEntry e in changedEntities)
            {
                ValidationContext validationContext = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(e.Entity, validationContext, errors, validateAllProperties: true);
            }

		  
		    
            if (errors.Any())
            {
                Debug.WriteLine(String.Join('\n', errors));
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IEnumerable<EntityEntry> changedEntities = ChangeTracker
                                                       .Entries()
                                                       .Where(_ => _.State == EntityState.Added || _.State == EntityState.Modified);

            List<ValidationResult> errors = new List<ValidationResult>(); // all errors are here
            foreach (EntityEntry e in changedEntities)
            {
                ValidationContext vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(e.Entity, vc, errors, validateAllProperties: true);
            }

            if (errors.Any())
            {
                Debug.WriteLine(String.Join('\n', errors));
            }

            return base.SaveChangesAsync(true, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Fight>(entity =>
            {
                entity.Property(e => e.ImagePath).IsUnicode(false);
            });

            modelBuilder.Entity<WatchCount>(entity =>
            {
                entity.HasKey(e => e.WikiFightId)
                      .HasName("PK_WatchCount_1");

                entity.Property(e => e.WikiFightId).ValueGeneratedNever();

                entity.HasOne(d => d.WikiFight)
                      .WithOne(p => p.WatchCountNavigation)
                      .HasForeignKey<WatchCount>(d => d.WikiFightId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_WatchCount_WikiFight");
            });

            modelBuilder.Entity<WikiFight>(entity =>
            {
                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Wikifight)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_wikifight_WikiEvent");

                entity.HasOne(d => d.Fight)
                    .WithMany(p => p.Wikifight)
                    .HasForeignKey(d => d.FightId)
                    .HasConstraintName("FK_wikifight_fight");
            });

            //modelBuilder.Entity<WikiEvent>(entity =>
            //{
            //    entity.HasMany(d => d.Wikifight)
            //          .WithOne(p => p.Event)
            //          .HasForeignKey(d => d.EventId)
            //          .HasConstraintName("FK_wikifight_WikiEvent");

            //    entity.HasOne(d => d.Fight)
            //          .WithMany(p => p.Wikifight)
            //          .HasForeignKey(d => d.FightId)
            //          .HasConstraintName("FK_wikifight_fight");
            //});

            modelBuilder.Entity<WikiFightWeb>(entity =>
            {
                entity.Property(e => e.ImageForWeb).IsUnicode(false);

                entity.Property(e => e.ImagePath).IsUnicode(false);

                //entity.HasOne(d => d.Event)
                //    .WithMany(p => p.WikifightWeb)
                //    .HasForeignKey(d => d.EventId)
                //    .HasConstraintName("FK_wikifightWeb_WikiEvent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}