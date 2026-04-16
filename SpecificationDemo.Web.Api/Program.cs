using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SpecificationDemo.Modulo.Clientes.Infraestrutura.EntityFramework;
using SpecificationDemo.Modulo.Clientes.InjecaoDeDependencia;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SpecificationDemo API",
        Version = "v1",
        Description = """
            Demonstração de **Specification Pattern** com EF Core e elegibilidade.

            ### Buscar clientes (`GET /api/clientes`)
            Parâmetros opcionais (combine cenários): `apenasAtivos`, `idadeMinima`, `scoreMinimo`.

            ### Elegibilidade cartão (`GET /api/clientes/{clienteId}/elegibilidade-cartao-adicional`)
            Parâmetros opcionais: `scoreMinimo` (default 650), `rendaMinima` (default 3000).

            ### GUIDs de seed
            | Cenário | `clienteId` |
            |--------|-------------|
            | Elegível (Ana) | `11111111-1111-1111-1111-111111111111` |
            | Score baixo (Bruno) | `22222222-2222-2222-2222-222222222222` |
            | Bloqueada (Carla) | `33333333-3333-3333-3333-333333333333` |
            | Menor (Daniel) | `44444444-4444-4444-4444-444444444444` |
            """,
    });
});

builder.Services.AddModuloClientes();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var contexto = scope.ServiceProvider.GetRequiredService<ContextoDeClientes>();
    await contexto.Database.EnsureCreatedAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SpecificationDemo v1");
        options.DocumentTitle = "SpecificationDemo — Swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
