using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Lab2.Model;

namespace Lab2.Database
{
    public partial class CinemaContext : DbContext
    {
        public CinemaContext()
        {
        }

        public CinemaContext(DbContextOptions<CinemaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Hall> Hall { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Seance> Seance { get; set; }
        public virtual DbSet<Seat> Seat { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Persist Security Info=true; Server=127.0.0.1; Port=5432; User Id=postgres; Password=aktriso4ka; Database=Cinema;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hall>(entity =>
            {
                entity.ToTable("hall");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Hall_id_seq\"'::regclass)");

                entity.Property(e => e.Format)
                    .IsRequired()
                    .HasColumnName("format")
                    .HasMaxLength(2)
                    .IsFixedLength();

                entity.Property(e => e.SeatsAmount).HasColumnName("seats_amount");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("movie");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Movie_id_seq\"'::regclass)");

                entity.Property(e => e.AgeLimit).HasColumnName("age_limit");

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasColumnName("country")
                    .HasMaxLength(20);

                entity.Property(e => e.ReleaseDate)
                    .HasColumnName("release_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Seance>(entity =>
            {
                entity.ToTable("seance");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Seance_id_seq\"'::regclass)");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Time)
                    .HasColumnName("time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.HallId).HasColumnName("hall_id");

                entity.Property(e => e.MovieId).HasColumnName("movie_id");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Seance)
                    .HasForeignKey(d => d.HallId)
                    .HasConstraintName("seance_hall_id_fkey");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Seance)
                    .HasForeignKey(d => d.MovieId)
                    .HasConstraintName("seance_movie_id_fkey");
            });

            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("seat");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Seat_id_seq\"'::regclass)");

                entity.Property(e => e.HallId).HasColumnName("hall_id");

                entity.Property(e => e.IsFree).HasColumnName("is_free");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.Row).HasColumnName("row");

                entity.HasOne(d => d.Hall)
                    .WithMany(p => p.Seat)
                    .HasForeignKey(d => d.HallId)
                    .HasConstraintName("seat_hall_id_fkey");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("ticket");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BuyTime)
                    .HasColumnName("buy_time")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.SeanceId).HasColumnName("seance_id");

                entity.Property(e => e.SeatId).HasColumnName("seat_id");

                entity.HasOne(d => d.Seance)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.SeanceId)
                    .HasConstraintName("ticket_seance_id_fkey");

                entity.HasOne(d => d.Seat)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.SeatId)
                    .HasConstraintName("ticket_seat_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
