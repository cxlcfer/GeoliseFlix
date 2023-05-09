using GeoliseFlix.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoliseFlix.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MovieComment> MovieComments { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieRating> MovieRatings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //FluentAPI
        //Personalizar as tabelas do identity
        builder.Entity<IdentityUser>(
            iu => { iu.ToTable("Users"); } //ToTable é usado para renomear uma tabela. 
        );

        builder.Entity<IdentityUserClaim<string>>( //Algumas entidades precisam que seu tipo seja especificado, como o string aqui.
           iu => { iu.ToTable("UserClaims"); }
       );

        builder.Entity<IdentityUserLogin<string>>(
           iu => { iu.ToTable("UserLogins"); }
       );

        builder.Entity<IdentityUserToken<string>>(
           iu => { iu.ToTable("UserTokens"); }
       );

        builder.Entity<IdentityRole>(
            iu => { iu.ToTable("Roles"); }
        );

        builder.Entity<IdentityRoleClaim<string>>(
            iu => { iu.ToTable("RoleClaims"); }
        );

        builder.Entity<IdentityUserRole<string>>(
       iu => { iu.ToTable("UserRoles"); }
   );

        #region Many to Many - MovieComment 
        //Muitos para muitos
        builder.Entity<MovieComment>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.Comments) 
                .HasForeignKey(mc => mc.MovieId);

        builder.Entity<MovieComment>()
                .HasOne(mc => mc.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(mc => mc.UserId);

        #endregion

        #region Many to Many - MovieGenre
        //Definição de Chave Primária Composta    
        builder.Entity<MovieGenre>().HasKey(
                mg => new { mg.MovieId, mg.GenreId } //Chave primária composta
        );

        builder.Entity<MovieGenre>()
               .HasOne(mg => mg.Movie)
               .WithMany(m => m.Genres)
               .HasForeignKey(mg => mg.MovieId);

        builder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(mg => mg.GenreId);
    
        #endregion

        #region Many to Many - MovieRating
       
        builder.Entity<MovieRating>().HasKey(
            mr => new { mr.MovieId, mr.UserId }
        );

        builder.Entity<MovieRating>()
                .HasOne(mr => mr.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(mr => mr.MovieId);

        builder.Entity<MovieRating>()
                .HasOne(mr => mr.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(mr => mr.UserId);
        #endregion    


    }


}
