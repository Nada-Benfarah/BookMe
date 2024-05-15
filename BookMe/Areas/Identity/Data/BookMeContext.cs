using BookMe.Areas.Identity.Data;
using BookMe.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookMe.Data;

public class BookMeContext : IdentityDbContext<BookMeUser>
{
    public BookMeContext(DbContextOptions<BookMeContext> options)
        : base(options)
    {
    }

    public DbSet<Auteur> Auteurs { get; set; }

    public DbSet<Copie> Copies { get; set; }
    public DbSet<Livre> Livres { get; set; }
    public DbSet<Theme> Themes { get; set; }
    public override DbSet<BookMeUser> Users { get; set; }
    public DbSet<LivreTheme> LivreThemes { get; set; }
    public DbSet<AuteurLivre> AuteurLivres { get; set; }
    public DbSet<UserCopie> UserCopies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LivreTheme>().HasKey(am => new
        {
            am.LivreId,
            am.ThemeId
        });



        modelBuilder.Entity<LivreTheme>().HasOne(m => m.Livre)
        .WithMany(am => am.LivreThemes).HasForeignKey(m => m.LivreId);

        modelBuilder.Entity<LivreTheme>().HasOne(a => a.Theme)
        .WithMany(at => at.LivreThemes).HasForeignKey(a => a.ThemeId);

        modelBuilder.Entity<AuteurLivre>().HasKey(am => new
        {
            am.AuteurId,
            am.LivreId
        });



        modelBuilder.Entity<AuteurLivre>().HasOne(m => m.Auteur)
        .WithMany(am => am.AuteurLivres).HasForeignKey(m => m.AuteurId);

        modelBuilder.Entity<AuteurLivre>().HasOne(a => a.Livre)
        .WithMany(at => at.AuteurLivres).HasForeignKey(a => a.LivreId);

        modelBuilder.Entity<LivreTheme>().HasOne(m => m.Livre)
        .WithMany(am => am.LivreThemes).HasForeignKey(m => m.LivreId);

        modelBuilder.Entity<LivreTheme>().HasOne(a => a.Theme)
        .WithMany(at => at.LivreThemes).HasForeignKey(a => a.ThemeId);

        modelBuilder.Entity<UserCopie>().HasKey(am => new
        {
            am.UserId,
            am.CopieId
        });



        modelBuilder.Entity<UserCopie>().HasOne(m => m.User)
        .WithMany(am => am.UserCopies).HasForeignKey(m => m.UserId);

        modelBuilder.Entity<UserCopie>().HasOne(a => a.Copie)
        .WithMany(at => at.UserCopies).HasForeignKey(a => a.CopieId);

        modelBuilder.ApplyConfiguration(new ApplyUserEntityConfiguration());


        base.OnModelCreating(modelBuilder);

    }




}

internal class ApplyUserEntityConfiguration : IEntityTypeConfiguration<BookMeUser>
{
    public void Configure(EntityTypeBuilder<BookMeUser> builder)
    {
        builder.Property(u => u.LastName).HasMaxLength(40);
        builder.Property(u => u.FirstName).HasMaxLength(40);
    }
}