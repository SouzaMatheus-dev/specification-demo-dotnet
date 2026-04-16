using MediatR;

namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.BuscarClientesPorFiltro;

/// <summary>
/// Query de leitura: o handler monta uma specification composta a partir dos parâmetros (spec-driven).
/// </summary>
public sealed record BuscarClientesPorFiltroQuery(
    bool? ApenasAtivos,
    int? IdadeMinima,
    decimal? ScoreMinimo) : IRequest<BuscarClientesPorFiltroResultado>;
