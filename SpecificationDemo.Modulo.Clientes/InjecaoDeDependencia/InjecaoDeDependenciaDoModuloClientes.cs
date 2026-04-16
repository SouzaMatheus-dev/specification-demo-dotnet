using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpecificationDemo.Modulo.Clientes.Infraestrutura.EntityFramework;

namespace SpecificationDemo.Modulo.Clientes.InjecaoDeDependencia;

public static class InjecaoDeDependenciaDoModuloClientes
{
    /// <summary>
    /// Extensão de DI do módulo: regista DbContext e handlers MediatR dos casos de uso.
    /// </summary>
    public static IServiceCollection AddModuloClientes(this IServiceCollection services)
    {
        services.AddDbContext<ContextoDeClientes>(options => options.UseInMemoryDatabase("SpecificationDemo.Clientes"));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ModuloClientesAnchor).Assembly));
        return services;
    }
}
