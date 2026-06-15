# Contexto Atual do Microservico Azure SQL para a Entrega 03

Este documento registra somente o que precisa ser considerado na `Entrega 03` sobre o microservico `People`, com base no estado atual deste repositorio.

## Papel na arquitetura da entrega

Este repositorio implementa o **Microservico 2** da solucao pedida em [entrega03.md](entrega03.md). Ele representa o dominio `People` e atende os requisitos da entrega para:

- microservico independente;
- banco relacional no **Azure SQL Database**;
- CRUD completo;
- documentacao Swagger/OpenAPI;
- organizacao com **Clean Architecture** e **Vertical Slice**;
- suporte a conteinerizacao com `Dockerfile`;
- testes de arquitetura.

## Estrutura do repositorio

O codigo esta dividido assim:

- `src/People.Domain`: entidade `Person` e contrato `IPersonRepository`.
- `src/People.Application`: casos de uso, DTOs, validacoes, mapeamentos e registro do MediatR/AutoMapper/FluentValidation.
- `src/People.Infrastructure`: `PeopleDbContext`, implementacao do repositorio e inicializacao do banco.
- `src/People.API`: controller REST, configuracao da API, Swagger e bootstrap da aplicacao.
- `tests/People.ArchitectureTests`: testes de arquitetura com ArchUnitNET.

## Modelo de dados persistido no Azure SQL

A entidade persistida e `Person`, mapeada para a tabela `People` em [src/People.Infrastructure/Persistence/PeopleDbContext.cs](src/People.Infrastructure/Persistence/PeopleDbContext.cs). Os campos atuais sao:

- `Id`
- `Name`
- `Role`
- `Department`
- `Email`
- `Status`
- `Summary`
- `CreatedAtUtc`
- `LastUpdatedAtUtc`

Restricoes aplicadas no mapeamento:

- `Name`: obrigatorio, maximo 150 caracteres.
- `Role`: obrigatorio, maximo 100 caracteres.
- `Department`: obrigatorio, maximo 100 caracteres.
- `Email`: obrigatorio, maximo 150 caracteres.
- `Status`: obrigatorio, maximo 20 caracteres, valor padrao `active`.
- `Summary`: maximo 500 caracteres.

## Como a conexao com Azure SQL funciona neste projeto

A conexao e registrada em [src/People.Infrastructure/DependencyInjection.cs](src/People.Infrastructure/DependencyInjection.cs) com `options.UseSqlServer(connectionString)`.

O projeto procura a string de conexao `ConnectionStrings:DefaultConnection` na configuracao da aplicacao. Hoje existe um valor base em [src/People.API/appsettings.json](src/People.API/appsettings.json), com placeholder de senha:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:srv-gestaorh-sql.database.windows.net,1433;Initial Catalog=gestaorh-people-db;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

Se essa string nao estiver presente, a API falha na inicializacao com `InvalidOperationException`.

## O que acontece na inicializacao da API

Na subida da aplicacao, [src/People.API/Program.cs](src/People.API/Program.cs):

- registra `Controllers`, `Swagger`, `Application` e `Infrastructure`;
- cria um escopo de servicos;
- resolve `PeopleDbContext`;
- executa `context.Database.EnsureCreated()`;
- executa `DbInitializer.Initialize(context)`.

Na pratica, isso significa:

- se o banco e a tabela ainda nao existirem, eles sao criados automaticamente;
- se a tabela `People` estiver vazia, sao inseridos 3 registros iniciais;
- se a tabela ja tiver dados, o seed nao roda novamente.

Os 3 registros iniciais atuais sao:

1. `Ana Souza` / `Analista RH` / `People`
2. `Carlos Lima` / `Tech Recruiter` / `People`
3. `Marina Costa` / `BP RH` / `Operations`

## API exposta por este microservico

O controller [src/People.API/Controllers/PeopleController.cs](src/People.API/Controllers/PeopleController.cs) publica os endpoints abaixo em `api/people`:

- `GET /api/people`
  Retorna uma colecao de `PersonSummaryDto` com `Id`, `Name`, `Role`, `Department`, `Email` e `Status`.
- `GET /api/people/{id}`
  Retorna `PersonDetailDto` com `Id`, `Name`, `Role`, `Department`, `Email`, `Status`, `Summary`, `LastUpdatedAtUtc` e `Source`.
- `POST /api/people`
  Cria uma pessoa e retorna `201 Created`.
- `PUT /api/people/{id}`
  Atualiza uma pessoa existente e retorna o objeto atualizado com `200 OK`. Se o ID nao existir, retorna `404 Not Found`.
- `DELETE /api/people/{id}`
  Remove o registro e retorna `204 No Content`. Se o ID nao existir, o handler apenas ignora e o controller continua retornando `204`.

Detalhe importante do retorno detalhado:

- o campo `Source` e preenchido no mapeamento com o valor fixo `azure-sql-service`.

## Validacoes implementadas

As validacoes atuais de `CreatePersonCommand` e `UpdatePersonCommand` garantem:

- `Name` obrigatorio e maximo 150;
- `Role` obrigatorio e maximo 100;
- `Department` obrigatorio e maximo 100;
- `Email` obrigatorio, formato valido e maximo 150;
- `Status` maximo 20;
- no update, `Id` maior que zero.

## Swagger e execucao local

Quando a API roda em ambiente `Development`, o Swagger fica habilitado.

As URLs configuradas em [src/People.API/Properties/launchSettings.json](src/People.API/Properties/launchSettings.json) sao:

- `http://localhost:5096`
- `https://localhost:7098`

Assim, o Swagger pode ser acessado em:

- `http://localhost:5096/swagger/index.html`
- `https://localhost:7098/swagger/index.html`

## Relacao com o restante da solucao da Entrega 03

Dentro da arquitetura pedida na entrega, este repositorio cobre somente o microservico de `People` com Azure SQL. Ele foi preparado para ser consumido por um BFF, que deve chamar este servico para:

- operacoes CRUD de `People`;
- agregacao de dados no endpoint consolidado do BFF.

O endpoint base que este repositorio expoe para integracao e:

- `http://localhost:5096/api/people`

## Testes de arquitetura

O projeto [tests/People.ArchitectureTests/CleanArchitectureTests.cs](tests/People.ArchitectureTests/CleanArchitectureTests.cs) valida regras estruturais do servico, incluindo:

- `Domain` sem dependencia de `Application`, `Infrastructure` ou `API`;
- `Application` sem dependencia de `Infrastructure` ou `API`;
- `Infrastructure` sem dependencia de `API`;
- handlers com sufixo `Handler`;
- controllers sem dependencia direta de repositories;
- entidades permanecendo na camada `Domain`.

## O que importa levar deste contexto para a entrega

Para a `Entrega 03`, este microservico ja atende o que precisa ser demonstrado no item de Azure SQL:

- usa Azure SQL como persistencia relacional;
- expõe CRUD completo de `People`;
- possui Swagger para demonstracao;
- segue Clean Architecture com Vertical Slice;
- pode ser executado localmente ou em container;
- esta pronto para compor o fluxo `Frontend -> BFF -> Microservico People`.
