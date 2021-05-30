using Microsoft.EntityFrameworkCore;
using System;

namespace MundoDisneyApiRest.Models
{
    public class DisneyContext : DbContext
    {
        public DisneyContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-OT37L3Q;Initial Catalog=DbMundoDisney;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
                
            base.OnConfiguring(optionsBuilder);

        }
        public DisneyContext(DbContextOptions<DisneyContext> options) : base(options)
        {

        }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenreMovieTVs> GenreMovieTVs { get; set; }
        public virtual DbSet<MovieTV> MovieTVs { get; set; }
        public virtual DbSet<MovieTVsCharacter> MovieTVsCharacters { get; set; }
        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");
                entity.HasKey(e => e.IdGenre);
                entity.Property(e => e.IdGenre).HasColumnName("idGenre");

                entity.Property(e => e.Imagen)
                    .HasColumnName("imagen")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasColumnName("nombre")
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieTV>(entity =>
            {
                entity.ToTable("MovieTV");
                entity.HasKey(e => e.IdMovieTV);
                entity.Property(e => e.IdMovieTV).HasColumnName("idMovieTV");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("titulo");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaCreacion")
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(e => e.Calificacion)
                    .HasDefaultValue(0)
                    .HasColumnName("calificacion");

                entity.HasMany(p => p.Genres)
                .WithMany(g => g.MovieTVs)
                .UsingEntity<GenreMovieTVs>(
                        pg => pg.HasOne(prop => prop.Genre)
                        .WithMany()
                        .HasForeignKey(prop => prop.IdGenre),
                        pg => pg.HasOne(prop => prop.MovieTV)
                        .WithMany()
                        .HasForeignKey(prop => prop.IdMovieTV),
                        pg =>
                        {
                            pg.HasKey(prop => new { prop.IdGenre, prop.IdMovieTV });
                        }
                    );

                entity.HasMany(p => p.Characters)
                .WithMany(ps => ps.MovieTVs)
                .UsingEntity<MovieTVsCharacter>(
                        pps => pps.HasOne(prop => prop.Character)
                        .WithMany()
                        .HasForeignKey(prop => prop.IdCharacter),
                        pps => pps.HasOne(prop => prop.MovieTV)
                        .WithMany()
                        .HasForeignKey(prop => prop.IdMovieTV),
                        pps =>
                        {
                            pps.HasKey(prop => new { prop.IdCharacter, prop.IdMovieTV });
                        }
                    );
            });


            modelBuilder.Entity<Character>(entity =>
            {
                entity.ToTable("Character");
                entity.HasKey(e => e.IdCharacter);
                entity.Property(e => e.IdCharacter).HasColumnName("idCharacter");

                entity.Property(e => e.Imagen)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imagen");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Edad)
                    .HasDefaultValue(0)
                    .HasColumnName("edad");

                entity.Property(e => e.Peso)
                    .HasDefaultValue(0)
                    .HasColumnType("decimal(3,1)")
                    .HasColumnName("peso");

                entity.Property(e => e.Historia)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("historia");
            });

            modelBuilder.Entity<MovieTVsCharacter>(entity =>
            {
                entity.HasKey(e => new { e.IdMovieTV, e.IdCharacter });

                entity.ToTable("MovieTVsCharacter");

                entity.HasIndex(e => e.IdCharacter, "IX_MovieTVsCharacter_CharactersIdCharacter");

                entity.HasOne(d => d.MovieTV)
                    .WithMany(p => p.Characterss)
                    .HasForeignKey(d => d.IdMovieTV)
                    .HasConstraintName("FK_MovieTVsCharacter_MovieTV_MovieTVsIdMovieTV");

                entity.HasOne(d => d.Character)
                    .WithMany(p => p.MoviesTVs)
                    .HasForeignKey(d => d.IdCharacter)
                    .HasConstraintName("FK_MovieTVsCharacter_Character_CharactersIdCharacter");
            });

            modelBuilder.Entity<GenreMovieTVs>(entity =>
            {
                entity.HasKey(e => new { e.IdGenre, e.IdMovieTV });

                entity.HasIndex(e => e.IdMovieTV, "IX_GenreMovieTVs_MovieTVsIdMovieTV");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.MoviesTVs)
                    .HasForeignKey(d => d.IdGenre)
                    .HasConstraintName("FK_GenreMovieTVs_Genre_GenresIdGenre");

                entity.HasOne(d => d.MovieTV)
                    .WithMany(p => p.Genress)
                    .HasForeignKey(d => d.IdMovieTV)
                    .HasConstraintName("FK_GenreMovieTVs_MovieTV_MovieTVsIdMovieTV");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.IdUser);
                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.UserName)
                    .HasColumnName("username")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mail)
                    .HasColumnName("mail")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
