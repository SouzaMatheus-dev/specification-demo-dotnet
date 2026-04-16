using System.Linq.Expressions;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.Dominio.Specifications;

public sealed class ScoreMinimoSpecification : Specification<Cliente>
{
    private readonly decimal _scoreMinimo;

    public ScoreMinimoSpecification(decimal scoreMinimo) => _scoreMinimo = scoreMinimo;

    public override Expression<Func<Cliente, bool>> ToExpression() => c => c.Score >= _scoreMinimo;
}
