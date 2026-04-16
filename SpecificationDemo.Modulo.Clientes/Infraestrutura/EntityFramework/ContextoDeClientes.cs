using Microsoft.EntityFrameworkCore;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.Infraestrutura.EntityFramework;

public sealed class ContextoDeClientes(DbContextOptions<ContextoDeClientes> options) : DbContext(options)
{
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(b =>
        {
            b.HasKey(c => c.Id);
            b.Property(c => c.Nome).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Cliente>().HasData(
            new Cliente
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Nome = "Ana Elegível",
                Ativo = true,
                Idade = 32,
                Score = 720,
                Bloqueado = false,
                RendaMensal = 5500,
            },
            new Cliente
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Nome = "Bruno Score Baixo",
                Ativo = true,
                Idade = 40,
                Score = 580,
                Bloqueado = false,
                RendaMensal = 8000,
            },
            new Cliente
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Nome = "Carla Bloqueada",
                Ativo = true,
                Idade = 29,
                Score = 800,
                Bloqueado = true,
                RendaMensal = 12000,
            },
            new Cliente
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Nome = "Daniel Menor",
                Ativo = true,
                Idade = 17,
                Score = 700,
                Bloqueado = false,
                RendaMensal = 4000,
            });
    }
}
