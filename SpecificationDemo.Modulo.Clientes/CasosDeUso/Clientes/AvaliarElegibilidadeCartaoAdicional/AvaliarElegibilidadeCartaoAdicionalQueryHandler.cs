using MediatR;
using Microsoft.EntityFrameworkCore;
using SpecificationDemo.Modulo.Clientes.Dominio.Clientes;
using SpecificationDemo.Modulo.Clientes.Dominio.Specifications;
using SpecificationDemo.Modulo.Clientes.Infraestrutura;
using SpecificationDemo.Modulo.Clientes.Infraestrutura.EntityFramework;

namespace SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.AvaliarElegibilidadeCartaoAdicional;

internal sealed class AvaliarElegibilidadeCartaoAdicionalQueryHandler(ContextoDeClientes contexto)
    : IRequestHandler<AvaliarElegibilidadeCartaoAdicionalQuery, AvaliarElegibilidadeCartaoAdicionalResultado>
{
    private readonly ContextoDeClientes _contexto = contexto;

    public async Task<AvaliarElegibilidadeCartaoAdicionalResultado> Handle(
        AvaliarElegibilidadeCartaoAdicionalQuery request,
        CancellationToken cancellationToken)
    {
        var cliente = await _contexto.Clientes.AsNoTracking().FirstOrDefaultAsync(c => c.Id == request.ClienteId, cancellationToken);
        if (cliente is null)
            throw new ClienteNaoEncontradoException(request.ClienteId);

        var regras = new (string MensagemSeFalhar, Specification<Cliente> Regra)[]
        {
            ("Cliente inativo.", new ClienteAtivoSpecification()),
            ("Cliente bloqueado.", new ClienteSemBloqueioSpecification()),
            ("Score abaixo do mínimo exigido.", new ScoreMinimoSpecification(request.ScoreMinimo)),
            ("Renda abaixo do mínimo exigido.", new RendaMinimaSpecification(request.RendaMinima)),
            ("Cliente menor de idade.", new ClienteMaiorDeIdadeSpecification()),
        };

        var motivos = new List<string>();
        foreach (var (mensagem, spec) in regras)
        {
            if (!spec.IsSatisfiedBy(cliente))
                motivos.Add(mensagem);
        }

        var composite = CartaoAdicionalElegivelSpecification.Criar(request.ScoreMinimo, request.RendaMinima);
        var elegivel = composite.IsSatisfiedBy(cliente);

        return new AvaliarElegibilidadeCartaoAdicionalResultado
        {
            Elegivel = elegivel,
            MotivosDeRecusa = motivos,
        };
    }
}
