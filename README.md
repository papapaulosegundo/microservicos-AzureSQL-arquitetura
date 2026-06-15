# 🌐 Microserviço de People (Azure SQL Architecture)

Este repositório contém o **Microserviço 2 (People)** do ecossistema de microserviços, projetado seguindo as práticas de **Clean Architecture** combinadas com **Vertical Slices** para o gerenciamento de dados de colaboradores. O backend utiliza o **ASP.NET Core Web API** integrado ao **Azure SQL Database** como banco de dados relacional.

A escolha do banco relacional Azure SQL para este domínio se deve à natureza estruturada dos dados de colaboradores, necessidade de integridade referencial, relacionamentos bem definidos (cargos, setores) e consistência transacional rígida (ACID).

---

## 🛠️ Tecnologias e Frameworks Utilizados

- **Core Framework:** .NET 9 / ASP.NET Core Web API
- **Banco de Dados:** Azure SQL Database
- **ORM & Acesso a Dados:** Entity Framework Core (EF Core)
- **Documentação de API:** Swagger / OpenAPI
- **Estruturação:** Clean Architecture & Vertical Slices
- **Testes de Arquitetura:** ArchUnitNET + xUnit (para validação automatizada de dependências entre camadas)
- **Containerização:** Docker

---

## 📐 Estrutura Arquitetural do Projeto

O microserviço é dividido em 4 camadas de código principais (`src/`) e 1 projeto de testes (`tests/`), estruturados de forma a manter a independência de frameworks e focar nas regras de negócio:

```text
microservicos-AzureSQL-arquitetura
│
├── 📁 src
│   ├── 📁 People.Domain          # Camada Central: Entidades (Person) e Interfaces de Repositório (IPersonRepository). Sem dependências externas.
│   ├── 📁 People.Application     # Camada de Aplicação: DTOs, Mapeamentos e Casos de Uso (CRUD) organizados por Vertical Slices.
│   ├── 📁 People.Infrastructure  # Camada de Infraestrutura: Contexto do EF Core (PeopleDbContext), Repositórios concretos e inicializador do BD.
│   └── 📁 People.API             # Camada de Apresentação: Controladores (Controllers), Configurações (appsettings.json) e Program.cs.
│
├── 📁 tests
│   └── 📁 People.ArchitectureTests # Testes estruturais automáticos que verificam o respeito às regras da Clean Architecture.
│
├── 📄 PeopleService.sln          # Solution principal do .NET
├── 📄 Dockerfile                 # Dockerfile de produção em múltiplos estágios
└── 📄 CONTEXTO_ETAPA_4_MICROSERVICO_SQL.md # Registro detalhado da decisão arquitetônica e contexto da entrega
```

### 🍕 Vertical Slices na Aplicação (`People.Application`)
Ao invés de pastas genéricas como `Services` ou `Handlers`, a camada de aplicação utiliza **Vertical Slices** (Fatias Verticais) para encapsular todas as operações do CRUD. Cada operação contém seus DTOs, validadores e lógica de negócios específicos:
- `CreatePerson`: Lógica para criação de colaborador.
- `GetPerson`: Busca detalhada por ID.
- `ListPeople`: Listagem otimizada de colaboradores.
- `UpdatePerson`: Atualização dos dados cadastrais.
- `DeletePerson`: Remoção lógica ou física do colaborador.

---

## 🔌 API Endpoints e CRUD

A API expõe os seguintes endpoints documentados:

| Método | Endpoint | Descrição | Payloads / Retornos |
| :--- | :--- | :--- | :--- |
| **GET** | `/api/people` | Retorna a lista de colaboradores ativos | Retorna um array de `PersonSummaryDto` |
| **GET** | `/api/people/{id}` | Busca os detalhes completos de um colaborador | Retorna um `PersonDetailDto` ou `404 Not Found` |
| **POST** | `/api/people` | Cadastra um novo colaborador no Azure SQL | Recebe `CreatePersonDto`, retorna a entidade criada com `201 Created` |
| **PUT** | `/api/people/{id}` | Atualiza as informações de um colaborador | Recebe `UpdatePersonDto`, retorna `204 No Content` ou `400/404` |
| **DELETE** | `/api/people/{id}` | Remove um colaborador do banco de dados | Retorna `204 No Content` ou `404 Not Found` |

---

## ⚡ Como Executar o Projeto

### 📋 Pré-requisitos
- .NET 9 SDK instalado.
- Acesso à internet ou uma instância configurada do SQL Server / Azure SQL Database.

### 1️⃣ Execução Local (dotnet CLI)

1. Clone o repositório e navegue até a pasta raiz:
   ```bash
   cd microservicos-AzureSQL-arquitetura
   ```
2. Restaure as dependências do projeto:
   ```bash
   dotnet restore PeopleService.sln
   ```
3. Configure a String de Conexão com seu banco Azure SQL no arquivo [appsettings.json](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.API/appsettings.json) ou use variáveis de ambiente.
4. Execute o projeto da API:
   ```bash
   dotnet run --project src/People.API/People.API.csproj
   ```
5. Acesse a documentação da API (Swagger UI) em:
   - **http://localhost:5096/swagger/index.html**
   - Ou via HTTPS: **https://localhost:7098/swagger/index.html**

> [!NOTE]
> O banco de dados e as tabelas são criadas automaticamente durante a inicialização do microserviço por meio do script de inicialização do EF Core. Colaboradores de teste fictícios também serão populados no banco (`DbInitializer.cs`) se o banco estiver vazio.

### 2️⃣ Execução via Docker

Para rodar o microserviço dentro de um container Docker isolado:

1. Construa a imagem Docker:
   ```bash
   docker build -t microservicos-peopleservice:latest .
   ```
2. Executa o container mapeando as portas e passando a string de conexão como variável de ambiente (substituindo pela senha real):
   ```bash
   docker run -d -p 5096:8080 -e ConnectionStrings__DefaultConnection="Server=tcp:srv-gestaorh-sql.database.windows.net,1433;Initial Catalog=gestaorh-people-db;User ID=sqladmin;Password=SUA_SENHA_AQUI;Encrypt=True;TrustServerCertificate=False;" --name peopleservice microservicos-peopleservice:latest
   ```
3. O endpoint estará exposto em `http://localhost:5096/api/people` e o Swagger em `http://localhost:5096/swagger/index.html`.

---

## 🧪 Testes de Arquitetura

Os testes de integridade arquitetural garantem que as regras de isolamento de camadas da Clean Architecture não sejam violadas ao longo do tempo (por exemplo, impedindo que a camada Domain dependa de Infrastructure ou API).

- Implementados no projeto [People.ArchitectureTests](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/tests/People.ArchitectureTests).
- Utilizam o framework **ArchUnitNET.xUnit**.

Para rodar os testes arquiteturais:
```bash
dotnet test tests/People.ArchitectureTests/People.ArchitectureTests.csproj
```

---

## 🗄️ Configurações Importantes

### String de Conexão com Azure SQL:
A configuração padrão de conexão reside no arquivo [appsettings.json](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.API/appsettings.json):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:srv-gestaorh-sql.database.windows.net,1433;Initial Catalog=gestaorh-people-db;Persist Security Info=False;User ID=sqladmin;Password=SUA_SENHA_AQUI;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}
```

### Integração com o BFF (Backend For Frontend):
Para integrar este microserviço com o BFF no projeto `backend-arquitetura-cloud`:
1. Defina `"UsePeopleMocks": false` no arquivo de configuração do BFF.
2. Altere o endereço downstream `"PeopleBaseUrl"` para o endereço deste microserviço (`http://localhost:5096/api/people/` ou o correspondente).

## Alunos

- Paulo César Muchalski
- Paulo Vitor
- Giulia Casteluci
- Juliano
- Gabriela Otte

## Publicacao no Docker Hub sem instalar Docker local

Este repositorio foi preparado para publicar a imagem pelo **GitHub Actions**, entao nao e preciso ter Docker instalado localmente para fazer o build e o push.

### Arquivos usados

- `.github/workflows/docker-people.yml`
- `.dockerignore`

### Como publicar

1. subir este codigo para o GitHub
2. abrir o repositorio no GitHub
3. ir em `Actions`
4. abrir o workflow `Publish People Microservice Image`
5. clicar em `Run workflow`

Tambem e possivel publicar automaticamente ao fazer push na branch padrao (`main` ou `master`).

### Tags publicadas

- `${DOCKERHUB_USERNAME}/pjbl-people:latest`
- `${DOCKERHUB_USERNAME}/pjbl-people:sha-<commit>`
