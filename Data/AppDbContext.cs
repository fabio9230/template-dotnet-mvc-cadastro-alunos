using CadastroAlunos.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroAlunos.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.CPF)
                .HasMaxLength(14)
                .IsRequired();

            entity.Property(x => x.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Telefone)
                .HasMaxLength(20);

            entity.Property(x => x.Status)
                .IsRequired();

            entity.Property(x => x.DataNascimento)
                .IsRequired();

            entity.HasIndex(x => x.CPF)
                .IsUnique();
        });
    }
}
