using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.BuscarClientesPorFiltro;

public sealed class BuscarClientesPorFiltroResultado
{
    public IReadOnlyList<ClienteResumoDto> Itens { get; init; } = Array.Empty<ClienteResumoDto>();
}

public sealed record ClienteResumoDto(Guid Id, string Nome, bool Ativo, int Idade, decimal Score, bool Bloqueado, decimal RendaMensal);
