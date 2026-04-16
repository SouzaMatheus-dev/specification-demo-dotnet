using MediatR;
using Microsoft.EntityFrameworkCore;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;
using SpecificationDemo.Modulo.Clientes.Dominio.Specifications;
using SpecificationDemo.Modulo.Clientes.Infraestrutura.EntityFramework;

namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.BuscarClientesPorFiltro;

internal sealed class BuscarClientesPorFiltroQueryHandler(ContextoDeClientes contexto)
    : IRequestHandler<BuscarClientesPorFiltroQuery, BuscarClientesPorFiltroResultado>
{
    private readonly ContextoDeClientes _contexto = contexto;

    public async Task<BuscarClientesPorFiltroResultado> Handle(BuscarClientesPorFiltroQuery request, CancellationToken cancellationToken)
    {
        Specification<Cliente> spec = new TodosOsClientesSpecification();

        if (request.ApenasAtivos is true)
            spec = spec.And(new ClienteAtivoSpecification());

        if (request.IdadeMinima is { } idadeMinima)
            spec = spec.And(new IdadeMinimaSpecification(idadeMinima));

        if (request.ScoreMinimo is { } scoreMinimo)
            spec = spec.And(new ScoreMinimoSpecification(scoreMinimo));

        var expressao = spec.ToExpression();
        var lista = await _contexto.Clientes.AsNoTracking().Where(expressao).OrderBy(c => c.Nome).ToListAsync(cancellationToken);

        return new BuscarClientesPorFiltroResultado
        {
            Itens = lista.Select(c => new ClienteResumoDto(c.Id, c.Nome, c.Ativo, c.Idade, c.Score, c.Bloqueado, c.RendaMensal)).ToList(),
        };
    }
}

internal sealed class TodosOsClientesSpecification : Specification<Cliente>
{
    public override System.Linq.Expressions.Expression<Func<Cliente, bool>> ToExpression() => _ => true;
}

internal sealed class IdadeMinimaSpecification : Specification<Cliente>
{
    private readonly int _idadeMinima;

    public IdadeMinimaSpecification(int idadeMinima) => _idadeMinima = idadeMinima;

    public override System.Linq.Expressions.Expression<Func<Cliente, bool>> ToExpression() => c => c.Idade >= _idadeMinima;
}
