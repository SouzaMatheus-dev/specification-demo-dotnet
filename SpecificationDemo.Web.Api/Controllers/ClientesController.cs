using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.AvaliarElegibilidadeCartaoAdicional;
using SpecificationDemo.Modulo.Clientes.CasosDeUso.Clientes.BuscarClientesPorFiltro;
using SpecificationDemo.Modulo.Clientes.Infraestrutura;

namespace SpecificationDemo.Web.Api.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    /// <summary>Listagem com filtros expressos como specifications compostas (EF Core).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(BuscarClientesPorFiltroResultado), StatusCodes.Status200OK)]
    public async Task<ActionResult<BuscarClientesPorFiltroResultado>> BuscarPorFiltro(
        [FromQuery] bool? apenasAtivos,
        [FromQuery] int? idadeMinima,
        [FromQuery] decimal? scoreMinimo,
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(new BuscarClientesPorFiltroQuery(apenasAtivos, idadeMinima, scoreMinimo), cancellationToken);
        return Ok(resultado);
    }

    /// <summary>Avalia elegibilidade (regras compostas + motivos de recusa por regra).</summary>
    [HttpGet("{clienteId:guid}/elegibilidade-cartao-adicional")]
    [ProducesResponseType(typeof(AvaliarElegibilidadeCartaoAdicionalResultado), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AvaliarElegibilidadeCartaoAdicionalResultado>> AvaliarElegibilidadeCartaoAdicional(
        Guid clienteId,
        [FromQuery] decimal scoreMinimo = 650,
        [FromQuery] decimal rendaMinima = 3000,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var resultado = await _sender.Send(
                new AvaliarElegibilidadeCartaoAdicionalQuery(clienteId, scoreMinimo, rendaMinima),
                cancellationToken);
            return Ok(resultado);
        }
        catch (ClienteNaoEncontradoException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}
