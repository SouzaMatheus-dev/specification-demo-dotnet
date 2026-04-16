# SpecificationDemo

Demonstração em **.NET 9** de **Specification Pattern** aplicado a **casos de uso** (consultas com EF Core e regras de elegibilidade em memória), com estrutura de pastas modular (módulo, domínio, infraestrutura, casos de uso).

> **Aviso:** repositório **apenas para fins educativos**. Cenários, nomes e números são **fictícios**; não representam produto, marca nem política comercial de qualquer organização.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Opcional: [Postman](https://www.postman.com/downloads/) ou compatível (Insomnia, etc.)

## Executar a API

```powershell
cd SpecificationDemo.Web.Api
dotnet run
```

Na primeira execução é criada a base **In-Memory** e os dados de exemplo são carregados automaticamente (`EnsureCreated` + `HasData`).

## Documentação

| Documento | Conteúdo |
|-----------|----------|
| [docs/FUNCIONAMENTO.md](docs/FUNCIONAMENTO.md) | Arquitetura, fluxo dos casos de uso, specifications e dados de seed |
| [docs/POSTMAN.md](docs/POSTMAN.md) | Como importar e usar a coleção Postman |

## Postman

- Coleção: [postman/SpecificationDemo.postman_collection.json](postman/SpecificationDemo.postman_collection.json)
- Ambiente local: [postman/SpecificationDemo.Local.postman_environment.json](postman/SpecificationDemo.Local.postman_environment.json)

Resumo: importar os dois ficheiros no Postman, selecionar o ambiente **SpecificationDemo Local** e executar os pedidos na pasta **Clientes**.

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/api/clientes` | Lista clientes com filtros opcionais (`apenasAtivos`, `idadeMinima`, `scoreMinimo`) via specifications compostas no EF |
| `GET` | `/api/clientes/{clienteId}/elegibilidade-cartao-adicional` | Avalia elegibilidade (score/renda configuráveis por query string) e devolve motivos de recusa |

**Swagger UI (Development):** com `ASPNETCORE_ENVIRONMENT=Development`, abre **`/swagger`** — interface para experimentar filtros na listagem, `scoreMinimo` / `rendaMinima` na elegibilidade e os GUIDs de seed descritos na descrição da API. Ao correr pelo Visual Studio / `dotnet run` com o perfil **http** ou **https**, o browser pode abrir diretamente no Swagger (`launchUrl` em `launchSettings.json`).

## Solução

Abrir `SpecificationDemo.sln` no Visual Studio (ou noutro IDE) e compilar:

```powershell
cd ..
dotnet build SpecificationDemo.sln
```

## Estrutura do repositório (resumo)

```
SpecificationDemo/
  README.md
  SpecificationDemo.sln
  docs/
    FUNCIONAMENTO.md
    POSTMAN.md
  postman/
    SpecificationDemo.postman_collection.json
    SpecificationDemo.Local.postman_environment.json
  SpecificationDemo.Modulo.Clientes/    # Domínio, EF, casos de uso (MediatR)
  SpecificationDemo.Web.Api/             # Host HTTP
```
