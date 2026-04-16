using System.Linq.Expressions;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.Dominio.Specifications;

public sealed class RendaMinimaSpecification : Specification<Cliente>
{
    private readonly decimal _rendaMinima;

    public RendaMinimaSpecification(decimal rendaMinima) => _rendaMinima = rendaMinima;

    public override Expression<Func<Cliente, bool>> ToExpression() => c => c.RendaMensal >= _rendaMinima;
}
