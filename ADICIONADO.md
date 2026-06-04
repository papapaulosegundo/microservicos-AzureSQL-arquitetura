# 📋 Itens Adicionados ao Repositório

Este documento detalha todos os componentes, projetos e arquivos que foram desenvolvidos e adicionados a este repositório para a implementação do **Microserviço de People** com banco de dados relacional **Azure SQL**.

---

## 🏗️ Estrutura e Projetos Adicionados

Foram criados 5 projetos estruturados sob o padrão de **Clean Architecture** com **Vertical Slices**:

### 1. Camada de Domínio (`src/People.Domain`)
Localizada em [src/People.Domain](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.Domain), esta camada não possui dependências de frameworks externos e contém o coração do domínio:
- **Entidade `Person`**: Define a estrutura de dados relacional do colaborador (ex: Id, Nome, Sobrenome, Cargo, Setor, E-mail, Salário, Data de Admissão, CPF, etc.).
- **Interface `IPersonRepository`**: Define o contrato abstrato para acesso a dados, desacoplando o domínio de detalhes de persistência.

### 2. Camada de Aplicação (`src/People.Application`)
Localizada em [src/People.Application](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.Application), contém as regras de caso de uso e operações da aplicação:
- **Fatias Verticais (Vertical Slices)**:
  - `CreatePerson`: Lógica e manipulação para criação de colaborador.
  - `GetPerson`: Caso de uso para obter detalhes por ID.
  - `ListPeople`: Caso de uso para listagem enxuta de colaboradores ativos.
  - `UpdatePerson`: Caso de uso para atualização de dados.
  - `DeletePerson`: Caso de uso para remoção do banco.
- **DTOs (Data Transfer Objects)**:
  - `PersonSummaryDto`: Dados simplificados para listas.
  - `PersonDetailDto`: Dados completos do colaborador.
  - `CreatePersonDto` / `UpdatePersonDto`: Estruturas de entrada de dados.
- **Dependency Injection**: Registro de serviços automático usando métodos de extensão.

### 3. Camada de Infraestrutura (`src/People.Infrastructure`)
Localizada em [src/People.Infrastructure](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.Infrastructure), realiza a integração externa com banco de dados:
- **`PeopleDbContext`**: Contexto do Entity Framework Core mapeando a entidade `Person` para a tabela do Azure SQL.
- **Implementação do Repositório (`PersonRepository`)**: Código concreto de CRUD utilizando EF Core.
- **Inicializador de Banco (`DbInitializer`)**: Garante que o banco seja criado e semeia dados iniciais (3 colaboradores fictícios para testes rápidos) se o banco de dados estiver vazio.

### 4. Camada de API (`src/People.API`)
Localizada em [src/People.API](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/src/People.API), atua como o ponto de entrada da aplicação Web:
- **`PeopleController`**: Controlador REST mapeando os verbos HTTP (`GET`, `POST`, `PUT`, `DELETE`) para os casos de uso do Application.
- **`Program.cs`**: Configuração geral do pipeline do ASP.NET Core, Injeção de Dependências e execução de scripts de migração automática do banco na inicialização.
- **Integração OpenAPI & Scalar**: Substituição da interface Swagger por uma interface moderna e bonita no caminho `/scalar/v1`.
- **Configurações (`appsettings.json`)**: Armazena as strings de conexão com o Azure SQL.

### 5. Camada de Testes de Arquitetura (`tests/People.ArchitectureTests`)
Localizada em [tests/People.ArchitectureTests](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/tests/People.ArchitectureTests), valida o isolamento de camadas:
- **Testes com ArchUnitNET**: 7 testes automatizados que garantem que as dependências respeitem as regras clássicas da Clean Architecture.

---

## 📄 Arquivos de Suporte Adicionados

- **[PeopleService.sln](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/PeopleService.sln)**: Solution principal do .NET conectando todos os projetos para desenvolvimento integrado no Visual Studio ou VS Code.
- **[Dockerfile](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/Dockerfile)**: Dockerfile otimizado em múltiplos estágios para construção de imagens leves prontas para produção.
- **[.gitignore](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/.gitignore)**: Arquivo padrão de exclusões do Git configurado para o ecossistema .NET.
- **[CONTEXTO_ETAPA_4_MICROSERVICO_SQL.md](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/CONTEXTO_ETAPA_4_MICROSERVICO_SQL.md)**: Documento de controle de contexto descrevendo a arquitetura adotada, conexão SQL configurada e próximos passos de integração com o BFF.
- **[README.md](file:///C:/Users/paulo/Desktop/projeto/microservicos-AzureSQL-arquitetura/README.md)**: Manual e guia completo de uso, execução, arquitetura e documentação do projeto.
