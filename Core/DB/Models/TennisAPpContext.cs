using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.DB.Models
{
    public partial class TennisAPpContext : DbContext
    {
        public TennisAPpContext()
        {
        }

        public TennisAPpContext(DbContextOptions<TennisAPpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TennisAPp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Club>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Result).IsUnicode(false);

                entity.HasOne(d => d.Player1)
                    .WithMany(p => p.MatchPlayer1)
                    .HasForeignKey(d => d.Player1Id)
                    .HasConstraintName("FK_Matches_Player1");

                entity.HasOne(d => d.Player2)
                    .WithMany(p => p.MatchPlayer2)
                    .HasForeignKey(d => d.Player2Id)
                    .HasConstraintName("FK_Matches_Player2");

                entity.HasOne(d => d.Player3)
                    .WithMany(p => p.MatchPlayer3)
                    .HasForeignKey(d => d.Player3Id)
                    .HasConstraintName("FK_Matches_Player3");

                entity.HasOne(d => d.Player4)
                    .WithMany(p => p.MatchPlayer4)
                    .HasForeignKey(d => d.Player4Id)
                    .HasConstraintName("FK_Matches_Player4");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.DoubleGrading).IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.SinglesGrading).IsUnicode(false);

                entity.Property(e => e.TennisNzId)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Player)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK_Players_Clubs");
            });
        }
    }
}
