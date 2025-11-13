ORBITA Career API Global Solution 2º Semestre -- Software Development C#

API RESTful em ASP.NET Core que faz parte do projeto ORBITA --
Laboratório de Carreiras do Futuro, desenvolvido para o Global Solution
-- O Futuro do Trabalho (FIAP).

A ORBITA Career API permite cadastrar rotas de carreira do futuro,
missões gamificadas e o progresso dos usuários, simulando a jornada de
desenvolvimento de competências em um cenário de trabalho remoto,
híbrido e intensivo em tecnologia e IA.

====================================================================
INTEGRANTES
====================================================================

\- Kaue Pastori Teixeira -- RM: 98501- Felipe Bressane
-- RM: 97688

Curso: Engenharia de Software -- FIAP Semestre: 2º semestre -- 2025

====================================================================
PROBLEMA E PROPÓSITO DA SOLUÇÃO
====================================================================

Pergunta norteadora: \"Como preparar pessoas para carreiras que ainda
nem existem, em um mercado de trabalho remoto, automatizado e guiado por
IA?\"

A transformação digital está criando novas profissões e redefinindo
funções tradicionais. Empresas e pessoas enfrentam desafios como:

\- Profissões e skills mudando em alta velocidade; - Formação
tradicional que não acompanha essas mudanças; - Times remotos e híbridos
exigindo novas competências; - Falta de um rastro estruturado do
desenvolvimento das pessoas.

Objetivo da ORBITA Career API:

\- Definir rotas de carreira do futuro (ex.: Arquiteto de IA Ética,
Especialista em Segurança de IA); - Cadastrar missões gamificadas que
simulam desafios reais dessas carreiras; - Registrar e acompanhar o
progresso dos usuários nessas missões.

No futuro, essa API pode ser consumida por: - Portais web / apps
mobile; - Plataformas de educação corporativa; - Assistentes de IA para
orientação de carreira.

====================================================================
CONEXÃO COM FUTURO DO TRABALHO E ODS
====================================================================

A solução dialoga com:

Futuro do Trabalho - Carreiras emergentes em IA, dados, automação, ESG,
segurança; - Trabalho remoto/híbrido com aprendizado contínuo; - Uso de
dados e IA para suportar decisões de RH e educação.

ODS impactados: - ODS 4 -- Educação de Qualidade; - ODS 8 -- Trabalho
Decente e Crescimento Econômico; - ODS 9 -- Indústria, Inovação e
Infraestrutura; - ODS 10 -- Redução das Desigualdades.

====================================================================
VISÃO GERAL DA ARQUITETURA
====================================================================

Tecnologias principais: - ASP.NET Core Web API (.NET 8); - C#; - Entity
Framework Core + SQL Server (LocalDB); - Swagger / OpenAPI; - API
Versioning (Microsoft.AspNetCore.Mvc.Versioning); - Postman (testes de
integração).

Camadas:

Models (Domínio) - Representam o coração do negócio:  - User  -
CareerPath  - Mission  - UserMissionProgress

Data -- OrbitaContext (EF Core) - Faz o mapeamento objeto--relacional; -
Define relacionamentos e configurações de banco.

Controllers - Interface HTTP da aplicação; - Recebem requisições, chamam
o contexto e retornam respostas JSON com status codes adequados.

Banco de dados: - OrbitaCareerDb (SQL Server), criado e mantido via
migrations.

====================================================================
MODELAGEM DE DADOS
====================================================================

USER - Id (Guid) -- identificador do usuário; - Name (string) -- nome
completo; - Email (string) -- e-mail; - CurrentRole (string) --
cargo/ocupação atual; - WeeklyAvailableHours (int) -- horas semanais
disponíveis para desenvolvimento.

CAREERPATH - Id (Guid); - Name (string) -- nome da carreira; - Area
(string) -- área (IA, Dados, Segurança, etc.); - Description (string) --
descrição detalhada; - Level (string) -- nível sugerido (Iniciante,
Intermediário, Avançado).

Relação: 1 CareerPath -\> N Mission.

MISSION - Id (Guid); - CareerPathId (Guid) -- chave estrangeira para
CareerPath; - Title (string); - Description (string); - Type (string) --
tipo de missão (Estudo, Projeto, Desafio Prático, etc.); - Difficulty
(int); - EstimatedMinutes (int); - XpReward (int).

USERMISSIONPROGRESS - Id (Guid); - UserId (Guid) -- FK para User; -
MissionId (Guid) -- FK para Mission; - Status (string) -- Pendente,
EmAndamento, Concluída; - CompletedAt (DateTime?) -- data/hora de
conclusão.

Relações: - 1 User -\> N UserMissionProgress; - 1 Mission -\> N
UserMissionProgress.

====================================================================
VERSIONAMENTO DA API
====================================================================

A API utiliza versionamento por URL, garantindo evolução sem quebrar
clientes existentes.

Versões disponíveis:

v1 -- versão principal - /api/v1/Users - /api/v1/CareerPaths -
/api/v1/Missions - /api/v1/Progress

v2 -- endpoint de exemplo - /api/v2/CareerPaths  - Retorna uma mensagem
simples com dados da versão 2 da API.

Configuração (resumo):

\- Uso de ApiVersioning com:  - ApiVersion(1,0) como padrão;  -
UrlSegmentApiVersionReader para ler a versão da URL;  - Rotas no
formato: api/v{version:apiVersion}/\[controller\].

====================================================================
ENDPOINTS PRINCIPAIS (V1) -- RESUMO
====================================================================

USERS - GET /api/v1/Users Lista todos os usuários. - GET
/api/v1/Users/{id} Retorna um usuário específico. - POST /api/v1/Users
Cria um novo usuário. - PUT /api/v1/Users/{id} Atualiza os dados de um
usuário. - DELETE /api/v1/Users/{id} Remove um usuário.

CAREERPATHS - GET /api/v1/CareerPaths Lista todas as rotas de
carreira. - GET /api/v1/CareerPaths/{id} Detalha uma rota específica. -
POST /api/v1/CareerPaths Cria uma nova rota de carreira. - PUT
/api/v1/CareerPaths/{id} Atualiza a rota. - DELETE
/api/v1/CareerPaths/{id} Remove a rota. - GET
/api/v1/CareerPaths/{id}/missions Lista missões associadas à rota.

MISSIONS - GET /api/v1/Missions Lista todas as missões (aceita
careerPathId como filtro). - GET /api/v1/Missions/{id} Detalha uma
missão específica. - POST /api/v1/Missions Cria uma nova missão. - PUT
/api/v1/Missions/{id} Atualiza uma missão. - DELETE
/api/v1/Missions/{id} Remove uma missão.

PROGRESS - GET /api/v1/Progress?userId={userId} Lista o progresso de um
usuário nas missões. - GET /api/v1/Progress/{id} Detalha um registro de
progresso. - POST /api/v1/Progress Registra início de missão para um
usuário. - PUT /api/v1/Progress/{id} Atualiza o status (por exemplo,
para Concluída). - DELETE /api/v1/Progress/{id} Remove um registro de
progresso.

====================================================================
FLUXO DA APLICAÇÃO (DRAW.IO)
====================================================================

O fluxo principal está documentado em: -
docs/fluxo-orbita-career-api.drawio

Fluxo exemplificado -- conclusão de missão:

1\. Cliente (Swagger, Postman ou front-end) envia POST /api/v1/Progress
informando UserId e MissionId; 2. ProgressController recebe a
requisição, valida dados e chama o OrbitaContext; 3. OrbitaContext
persiste o registro em UserMissionProgress no banco SQL Server; 4. API
retorna 201 Created; 5. Ao concluir a missão, o cliente envia PUT
/api/v1/Progress/{id} com Status = Concluída e CompletedAt; 6. API
atualiza o registro e retorna 204 No Content; 7. Consultas posteriores a
GET /api/v1/Progress?userId=\... retornam o histórico de jornada do
usuário.

====================================================================
DOCUMENTAÇÃO DA API (SWAGGER)
====================================================================

A documentação é gerada via Swagger e exposta na raiz do projeto.

Após rodar a aplicação:

\- URL típica: https://localhost:7180/

Na interface Swagger é possível: - Visualizar todos os endpoints por
versão; - Ver estrutura dos modelos; - Executar chamadas de teste
(GET/POST/PUT/DELETE); - Verificar status codes e payloads.

====================================================================
COMO EXECUTAR O PROJETO
====================================================================

Pré-requisitos: - .NET 8 SDK instalado; - SQL Server / SQL Server
Express LocalDB; - (Opcional) Postman para testes.

Passos para execução:

1\. Clonar o repositório: git clone
https://github.com/KauePastori/orbita-career-api.git cd
orbita-career-api

2\. Verificar a connection string em appsettings.json:

\"ConnectionStrings\": { \"DefaultConnection\":
\"Server=(localdb)\\\\MSSQLLocalDB;Database=OrbitaCareerDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;\"
}

(Ajustar se estiver usando outra instância de SQL Server.)

3\. Aplicar migrations do Entity Framework Core:

Via Package Manager Console (Visual Studio): Add-Migration InitialCreate
\# se ainda não existir Update-Database

ou via CLI: dotnet ef database update

4\. Executar a API: dotnet run

5\. Acessar o Swagger: Abrir https://localhost:7180/ (ou porta exibida
no console).

====================================================================
TESTES COM POSTMAN
====================================================================

Existe uma collection de testes automatizados para validar a API:

\- Arquivo: docs/Orbita_CareerApi.postman_collection.json

Fluxo coberto pela collection: 1. Criação de CareerPath; 2. Listagem de
CareerPaths; 3. Criação de Mission associada; 4. Listagem de Missions
por CareerPath; 5. Criação de User; 6. Criação de Progress (início de
missão); 7. Atualização do Progress para Concluída; 8. Listagem do
Progress do usuário.

====================================================================
FORMA DE FUNCIONAMENTO -- RESUMO
====================================================================

\- Rotas de carreira representam trajetórias profissionais do futuro; -
Missões gamificadas simulam desafios reais dessas carreiras; - Usuários
são cadastrados e conectados às missões; - Progresso de cada missão é
registrado e atualizado na API; - Empresas e educadores podem, a partir
dos dados, analisar o desenvolvimento de competências ligadas ao Futuro
do Trabalho.

====================================================================
VÍDEO DE DEMONSTRAÇÃO
====================================================================

Link do vídeo (YouTube ou equivalente), máximo 5 minutos:

LINK DO VÍDEO:
\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_\_

Sugestão de roteiro: - Apresentação dos integrantes e do contexto da
GS; - Explicação rápida da ideia da ORBITA e das entidades; -
Demonstração no Swagger do fluxo completo (CareerPath, Mission, User,
Progress); - Exemplo de chamada à versão 2 (api/v2/CareerPaths); -
Comentário sobre o uso da collection do Postman e do diagrama Draw.io.
