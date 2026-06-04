# Contexto da Etapa 4 - Microserviço 2 Azure SQL

Este arquivo registra o contexto completo do que foi feito no item 4 da atividade para manter continuidade nas próximas conversas, nas próximas entregas e na integração com o BFF.

## Objetivo desta etapa

Implementar o **Microserviço 2** da arquitetura distribuída, usando:

- Banco de dados relacional **Azure SQL**
- Domínio independente para **People**
- CRUD completo
- Swagger/OpenAPI documentado com Scalar
- Clean Architecture com Vertical Slices

## Decisão arquitetural adotada

O domínio `People` foi escolhido para o microserviço Azure SQL porque combina melhor com armazenamento relacional:

- Dados estruturados de colaboradores.
- Relacionamentos fixos (cargos, setores).
- Integridade referencial.
- Consistência transacional (ACID).

Essa divisão se alinha ao restante do ecossistema:

- O front-end possui o microfrontend `mfe-people`.
- O BFF possui o endpoint `/people` e integra no `/aggregated-data`.

## Estrutura criada no repositório

```text
microservicos-AzureSQL-arquitetura
|-- src
|   |-- People.Domain          # Entidades (Person) e IPersonRepository
|   |-- People.Application     # DTOs, Mapping, Slices (List, Get, Create, Update, Delete)
|   |-- People.Infrastructure  # PeopleDbContext, PersonRepository, DbInitializer
|   `-- People.API             # Controllers (PeopleController), Program.cs, appsettings.json
|-- tests
|   `-- People.ArchitectureTests # Testes com ArchUnitNET.xUnit
|-- PeopleService.sln          # Solution .NET
|-- Dockerfile                 # Publicação em container
|-- .gitignore                 # Arquivos ignorados no git
`-- CONTEXTO_ETAPA_4_MICROSERVICO_SQL.md # Este arquivo de contexto
```

## CRUD e Endpoints implementados

- `GET /api/people` -> Lista de colaboradores (`PersonSummaryDto`).
- `GET /api/people/{id}` -> Detalhes do colaborador por ID (`PersonDetailDto`).
- `POST /api/people` -> Criação de novo colaborador.
- `PUT /api/people/{id}` -> Atualização de colaborador.
- `DELETE /api/people/{id}` -> Exclusão de colaborador.

## Integração com Azure SQL

- Gerenciado via **Entity Framework Core (EF Core)**.
- O script de inicialização `DbInitializer.cs` roda na inicialização do `Program.cs` (`context.Database.EnsureCreated()`), gerando automaticamente o banco e tabelas caso não existam no Azure SQL, e semeando os mesmos 3 colaboradores fictícios do mock inicial.

### String de conexão configurada em appsettings.json:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:srv-gestaorh-sql.database.windows.net,1433;Initial Catalog=gestaorh-people-db;Persist Security Info=False;User ID=sqladmin;Password=SUA_SENHA_AQUI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

## Testes de Arquitetura

- Implementados em `tests/People.ArchitectureTests` utilizando o framework `TngTech.ArchUnitNET.xUnit`.
- Valida o isolamento das camadas da Clean Architecture e a separação de responsabilidades (ex: controllers não dependendo diretamente de repositories).
- Total de **7 testes aprovados com sucesso**.

## Próximo Passo: Integração com o BFF

1. No repositório do BFF (`backend-arquitetura-cloud`):
   - Atualizar a configuração `DownstreamServicesOptions` em `appsettings.json`:
     - `"UsePeopleMocks": false`
     - `"PeopleBaseUrl": "http://localhost:5101/api/people/"`
2. Testar e validar o fluxo completo: `Microfrontend -> BFF -> Microserviço 2 (Azure SQL)`.
