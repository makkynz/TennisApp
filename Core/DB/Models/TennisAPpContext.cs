using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.DB.Models
{
    public partial class TennisAppContext : DbContext
    {
        public TennisAppContext()
        {
        }

        public TennisAppContext(DbContextOptions<TennisAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=TennisApp;Trusted_Connection=True;");
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
                    .WithMany(p => p.MatchPlayer1s)
                    .HasForeignKey(d => d.Player1Id)
                    .HasConstraintName("FK_Matches_Player1");

                entity.HasOne(d => d.Player2)
                    .WithMany(p => p.MatchPlayer2s)
                    .HasForeignKey(d => d.Player2Id)
                    .HasConstraintName("FK_Matches_Player2");

                entity.HasOne(d => d.Player3)
                    .WithMany(p => p.MatchPlayer3s)
                    .HasForeignKey(d => d.Player3Id)
                    .HasConstraintName("FK_Matches_Player3");

                entity.HasOne(d => d.Player4)
                    .WithMany(p => p.MatchPlayer4s)
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

                entity.Property(e => e.Woot)
                    .HasColumnName("woot")
                    .IsUnicode(false);

                entity.HasOne(d => d.Club)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.ClubId)
                    .HasConstraintName("FK_Players_Clubs");
            });
        }
    }
}
