namespace SpecificationDemo.Modulo.Clientes.Infraestrutura;

public sealed class ClienteNaoEncontradoException : Exception
{
    public ClienteNaoEncontradoException(Guid clienteId)
        : base($"Cliente '{clienteId}' não encontrado.") { }
}
