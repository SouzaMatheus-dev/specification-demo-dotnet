using System.Linq.Expressions;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.Dominio.Specifications;

public sealed class ClienteAtivoSpecification : Specification<Cliente>
{
    public override Expression<Func<Cliente, bool>> ToExpression() => c => c.Ativo;
}
