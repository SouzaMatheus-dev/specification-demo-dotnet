namespace SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

/// <summary>
/// Entidade de exemplo para explorar specifications em consultas e regras de elegibilidade.
/// </summary>
public sealed class Cliente
{
    public Guid Id { get; init; }

    public string Nome { get; init; } = string.Empty;

    public bool Ativo { get; init; }

    public int Idade { get; init; }

    public decimal Score { get; init; }

    public bool Bloqueado { get; init; }

    public decimal RendaMensal { get; init; }
}
