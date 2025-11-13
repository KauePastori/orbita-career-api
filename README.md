ORBITA Career API – Global Solution 2º Semestre (Software Development C#)

\=========================================================================

API RESTful em ASP.NET Core que faz parte do projeto ORBITA – Laboratório de Carreiras do Futuro, desenvolvido para o Global Solution – O Futuro do Trabalho.

A ORBITA Career API permite cadastrar rotas de carreira do futuro, missões gamificadas e o progresso dos usuários, simulando a jornada de preparação para profissões emergentes em um mundo de trabalho remoto, híbrido, conectado e impactado por IA.

\------------------------------------------------------------------------

INTEGRANTES

\------------------------------------------------------------------------

- Kaue Pastori Teixeira – RA XXXXXXXX
- Felipe Bressane – RA XXXXXXXX

\------------------------------------------------------------------------

CONTEXTO E PROPÓSITO DA SOLUÇÃO

\------------------------------------------------------------------------

“Como preparar pessoas para carreiras que ainda nem existem?”

A ORBITA nasce como um laboratório de carreiras do futuro. A ideia é fornecer uma base tecnológica para que empresas, universidades e hubs de inovação criem:

- Rotas de carreira focadas em profissões emergentes (IA, dados, ESG, segurança, automação);
- Missões gamificadas, que simulam desafios reais do trabalho;
- Acompanhamento de progresso dos profissionais ao longo do tempo.

A solução responde diretamente ao desafio do Futuro do Trabalho:

- Profissões mudando rápido;
- Necessidade de aprendizado contínuo;
- Ambientes remotos e híbridos;
- Uso intensivo de IA e dados para tomar decisão.

Conexão com ODS:

- ODS 4 – Educação de Qualidade
- ODS 8 – Trabalho Decente e Crescimento Econômico
- ODS 9 – Indústria, Inovação e Infraestrutura
- ODS 10 – Redução das Desigualdades

\------------------------------------------------------------------------

TECNOLOGIAS UTILIZADAS

\------------------------------------------------------------------------

Back-end

- .NET 8 / ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server (LocalDB)

Documentação e Testes

- Swagger / OpenAPI
- Postman (collection de testes automatizados)
- Draw.io (diagrama de fluxo da aplicação)

Versionamento de API

- Microsoft.AspNetCore.Mvc.Versioning

\------------------------------------------------------------------------

ARQUITETURA DA SOLUÇÃO

\------------------------------------------------------------------------

A solução é organizada em camadas simples:

Controllers

- Recebem as requisições HTTP, aplicam validações básicas e convertem o resultado em respostas REST (status code + JSON).
- Principais controllers:
- UsersController
- CareerPathsController
- MissionsController
- ProgressController
- CareerPathsV2Controller (exemplo de versão 2)

Data (OrbitaContext)

- Contexto do Entity Framework Core, responsável pelo mapeamento das entidades para o banco SQL Server.
- Relacionamentos:
- CareerPath 1:N Mission
- User 1:N UserMissionProgress
- Mission 1:N UserMissionProgress

Models (Domínio)

- User
- CareerPath
- Mission
- UserMissionProgress

Banco de dados: OrbitaCareerDb, criado e versionado via migrations do EF Core.

\------------------------------------------------------------------------

MODELAGEM DE DADOS

\------------------------------------------------------------------------

User

- Id (Guid)
- Name
- Email
- CurrentRole (cargo/ocupação atual)
- WeeklyAvailableHours (horas semanais disponíveis para estudo/desenvolvimento)

CareerPath

- Id (Guid)
- Name
- Area (IA, Dados, DevOps etc.)
- Description
- Level (Iniciante, Intermediário, Avançado)
- Missions (coleção de Missão)

Mission

- Id (Guid)
- CareerPathId (chave estrangeira)
- Title
- Description
- Type (Estudo, Projeto, Desafio Prático, etc.)
- Difficulty (0–5)
- EstimatedMinutes
- XpReward

UserMissionProgress

- Id (Guid)
- UserId
- MissionId
- Status (Pendente, EmAndamento, Concluída)
- CompletedAt (data/hora quando concluída)

\------------------------------------------------------------------------

VERSIONAMENTO DA API

\------------------------------------------------------------------------

A API utiliza versionamento por segmento de URL, garantindo evolução sem quebrar integrações existentes.

Versão 1 (v1)

- Endpoints oficiais:
- /api/v1/Users
- /api/v1/CareerPaths
- /api/v1/Missions
- /api/v1/Progress

Versão 2 (v2)

- Endpoint demonstrativo:
- /api/v2/CareerPaths
- Retorna uma mensagem simples mostrando que a versão 2 está configurada.

Configuração principal de versionamento (exemplo de código):

builder.Services.AddApiVersioning(options =>

{

options.DefaultApiVersion = new ApiVersion(1, 0);

options.AssumeDefaultVersionWhenUnspecified = true;

options.ReportApiVersions = true;

options.ApiVersionReader = new UrlSegmentApiVersionReader();

});

Rotas dos controllers (exemplo):

[ApiController]

[ApiVersion("1.0")]

[Route("api/v{version:apiVersion}/[controller]")]

public class CareerPathsController : ControllerBase

{

// ...

}

\------------------------------------------------------------------------

ENDPOINTS PRINCIPAIS (V1)

\------------------------------------------------------------------------

USERS

- GET  /api/v1/Users

Lista todos os usuários.

- GET  /api/v1/Users/{id}

Retorna um usuário específico.

- POST /api/v1/Users

Cria um novo usuário.

- PUT  /api/v1/Users/{id}

Atualiza um usuário existente.

- DELETE /api/v1/Users/{id}

Remove um usuário.

CAREERPATHS

- GET  /api/v1/CareerPaths

Lista todas as rotas de carreira.

- GET  /api/v1/CareerPaths/{id}

Detalhes de uma rota específica (inclui missões).

- POST /api/v1/CareerPaths

Cria uma nova rota de carreira.

- PUT  /api/v1/CareerPaths/{id}

Atualiza a rota.

- DELETE /api/v1/CareerPaths/{id}

Remove a rota.

- GET  /api/v1/CareerPaths/{id}/missions

Lista as missões ligadas à rota.

MISSIONS

- GET  /api/v1/Missions

Lista todas as missões (opcionalmente filtradas por careerPathId).

- GET  /api/v1/Missions/{id}

Detalhes de uma missão específica.

- POST /api/v1/Missions

Cria uma nova missão.

- PUT  /api/v1/Missions/{id}

Atualiza uma missão.

- DELETE /api/v1/Missions/{id}

Remove uma missão.

PROGRESS

- GET  /api/v1/Progress?userId={userId}

Lista o progresso de todas as missões de um usuário.

- GET  /api/v1/Progress/{id}

Detalhes de um registro de progresso.

- POST /api/v1/Progress

Registra que um usuário iniciou uma missão.

- PUT  /api/v1/Progress/{id}

Atualiza o status (por exemplo, para Concluída).

- DELETE /api/v1/Progress/{id}

Remove o registro de progresso.

\------------------------------------------------------------------------

FLUXO DA APLICAÇÃO (DRAW.IO)

\------------------------------------------------------------------------

O fluxo principal da aplicação está representado no arquivo:

- docs/fluxo-orbita-career-api.drawio

Fluxo ilustrado:

1. Cliente (Swagger, Postman ou futura aplicação front-end) envia uma requisição HTTP para a API.
1. O controller correspondente recebe a requisição (por exemplo, ProgressController).
1. O controller utiliza o OrbitaContext (EF Core) para consultar ou persistir dados no SQL Server.
1. O resultado é retornado ao cliente como JSON, com status codes adequados (200, 201, 204, 400, 404 etc.).

Exemplos de fluxos modelados:

- Criação de rota de carreira (POST /api/v1/CareerPaths).
- Criação de missão ligada a uma rota (POST /api/v1/Missions).
- Registro e conclusão de progresso (POST e PUT /api/v1/Progress).

\------------------------------------------------------------------------

DOCUMENTAÇÃO DA API (SWAGGER)

\------------------------------------------------------------------------

A documentação da API é gerada automaticamente via Swagger e exposta na raiz da aplicação:

- https://localhost:7180/  (ou porta configurada)

Através do Swagger é possível:

- Visualizar todos os endpoints e modelos.
- Executar requisições de teste diretamente pela interface.
- Conferir exemplos de request/response em JSON.

\------------------------------------------------------------------------

COMO EXECUTAR O PROJETO

\------------------------------------------------------------------------

Pré-requisitos:

- .NET 8 SDK
- SQL Server / SQL Server Express LocalDB
- (Opcional) Postman

Passos:

1. Clonar o repositório:

git clone <URL\_DO\_REPOSITORIO>

cd Orbita.CareerApi

1. Verificar a connection string em appsettings.json:

"ConnectionStrings": {

"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=OrbitaCareerDb;Trusted\_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"

}

(Ajustar se estiver usando outra instância de SQL Server.)

1. Aplicar as migrations do EF Core:

Via Package Manager Console (Visual Studio):

Add-Migration InitialCreate   # se ainda não existir migration

Update-Database

ou via CLI:

dotnet ef database update

1. Executar a API:

dotnet run

1. Acessar o Swagger:

Abrir o navegador em https://localhost:7180/  (ou porta exibida no console).

\------------------------------------------------------------------------

TESTES COM POSTMAN

\------------------------------------------------------------------------

Uma collection do Postman foi criada para validar o fluxo completo da API:

- Arquivo: docs/Orbita\_CareerApi.postman\_collection.json

Fluxo automatizado da collection:

1. Criar uma CareerPath.
1. Listar CareerPaths.
1. Criar uma Mission para a CareerPath.
1. Listar Missions por CareerPath.
1. Criar um User.
1. Criar um Progress (usuário inicia missão).
1. Atualizar Progress para "Concluída".
1. Listar Progress do usuário.

\------------------------------------------------------------------------

FORMA DE FUNCIONAMENTO (RESUMO)

\------------------------------------------------------------------------

- Rotas de carreira representam trajetórias profissionais do futuro.
- Cada rota possui missões gamificadas que simulam desafios reais.
- Usuários são cadastrados na API e podem avançar nessas missões.
- A cada missão iniciada ou concluída, é criado/atualizado um registro em UserMissionProgress.
- A partir desses dados, empresas e educadores podem acompanhar o desenvolvimento de competências voltadas ao Futuro do Trabalho.

\------------------------------------------------------------------------

VÍDEO DE DEMONSTRAÇÃO

\------------------------------------------------------------------------

Link do vídeo (YouTube ou equivalente) demonstrando a solução integrada (máx. 5 minutos):

- LINK DO VÍDEO: \_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_

Sugestão de roteiro:

1. Apresentação rápida dos integrantes e do contexto do Futuro do Trabalho.
1. Explicação da ideia da ORBITA e das entidades principais.
1. Demonstração no Swagger (fluxo completo de criação de rota, missão, usuário e progresso).
1. Destaque para o versionamento da API (v1 e v2).
1. Citar o uso do Postman para testes automatizados e o diagrama Draw.io para documentação do fluxo.
