namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.AvaliarElegibilidadeCartaoAdicional;

public sealed class AvaliarElegibilidadeCartaoAdicionalResultado
{
    public bool Elegivel { get; init; }

    public IReadOnlyList<string> MotivosDeRecusa { get; init; } = Array.Empty<string>();
}
