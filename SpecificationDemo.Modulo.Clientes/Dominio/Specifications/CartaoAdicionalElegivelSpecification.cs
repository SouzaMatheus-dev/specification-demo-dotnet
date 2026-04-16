using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;

namespace SpecificationDemo.Modulo.Clientes.Dominio.Specifications;

/// <summary>
/// Composição de regras de negócio (exemplo bancário do teu texto) como specification única reutilizável.
/// </summary>
public static class CartaoAdicionalElegivelSpecification
{
    public static Specification<Cliente> Criar(decimal scoreMinimo, decimal rendaMinima) =>
        new ClienteAtivoSpecification()
            .And(new ClienteSemBloqueioSpecification())
            .And(new ScoreMinimoSpecification(scoreMinimo))
            .And(new RendaMinimaSpecification(rendaMinima))
            .And(new ClienteMaiorDeIdadeSpecification());
}
