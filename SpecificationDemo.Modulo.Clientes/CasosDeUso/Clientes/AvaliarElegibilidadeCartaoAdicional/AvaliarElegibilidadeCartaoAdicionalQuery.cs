using MediatR;

namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.AvaliarElegibilidadeCartaoAdicional;

/// <summary>
/// Caso de uso orientado a specifications: avalia regras compostas sobre uma entidade carregada.
/// </summary>
public sealed record AvaliarElegibilidadeCartaoAdicionalQuery(
    Guid ClienteId,
    decimal ScoreMinimo,
    decimal RendaMinima) : IRequest<AvaliarElegibilidadeCartaoAdicionalResultado>;
