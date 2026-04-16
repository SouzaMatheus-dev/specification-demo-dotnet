# Postman — SpecificationDemo

Coleção para **testes locais** do exemplo educativo (dados fictícios).

## Importar

1. Abrir o Postman.
2. **Import** → arrastar ou selecionar:
   - `postman/SpecificationDemo.postman_collection.json`
   - `postman/SpecificationDemo.Local.postman_environment.json`

## Ambiente

No canto superior direito, escolher o ambiente **SpecificationDemo Local**.

Variável predefinida:

| Variável | Valor por omissão |
|----------|-------------------|
| `baseUrl` | `http://localhost:5103` |

Se a API correr em outra porta ou HTTPS, altere `baseUrl` no ambiente (ex.: `https://localhost:7178`).

## Executar a API antes dos pedidos

```powershell
cd SpecificationDemo.Web.Api
dotnet run
```

Confirme no consola a URL em escuta (ex.: `http://localhost:5103`).

## Coleção **SpecificationDemo**

A pasta **Clientes** contém:

| Pedido | Objetivo |
|--------|----------|
| Buscar clientes (sem filtros) | Lista os quatro registos de seed |
| Buscar clientes (ativos + score ≥ 650) | Exemplo de composição no servidor (Bruno fica de fora) |
| Elegibilidade — Ana (elegível) | `elegivel: true`, sem motivos |
| Elegibilidade — Bruno (score) | Recusa por score |
| Elegibilidade — Carla (bloqueio) | Recusa por bloqueio |
| Elegibilidade — Daniel (menor) | Recusa por idade |
| Elegibilidade — cliente inexistente | Esperado **404** |

Os GUIDs dos clientes estão fixos na coleção; basta **Send** em cada pedido.

## Problemas comuns

- **Connection refused:** a API não está a correr ou a porta não coincide com `baseUrl`.
- **404 em todos os GET:** verificar se o caminho inclui `/api/clientes` (a coleção já inclui).
- **SSL / HTTPS:** se usar `https://localhost:7178`, pode ser necessário desativar verificação SSL no Postman (apenas em desenvolvimento local).
