# ORBITA Career API (ASP.NET Core + EF Core)

> API Web em **ASP.NET Core 8** com **Entity Framework Core (SQL Server)**, **Swagger/OpenAPI**, **versionamento de API** e um modelo de **gamificação de carreiras** voltado ao tema **“Futuro do Trabalho”** da Global Solution FIAP.  
> Parte do projeto **ORBITA – Laboratório de Carreiras do Futuro**.

---

## ✨ Destaques

- **Domínio real**: rotas de carreira do futuro, missões gamificadas e progresso dos usuários
- **CRUD completo** para:
  - `Users`
  - `CareerPaths`
  - `Missions`
  - `UserMissionProgress` (Progress)
- **API RESTful**:
  - Verbos HTTP corretos (GET, POST, PUT, DELETE)
  - Status codes adequados (200, 201, 204, 400, 404)
- **Versionamento de API**:
  - `v1` – API principal
  - `v2` – endpoint demonstrativo (`/api/v2/CareerPaths`)
- **Swagger/OpenAPI** com UI de testes e documentação automática
- **EF Core + SQL Server LocalDB** com relacionamentos configurados
- **Collection Postman** com fluxo de teste ponta a ponta
- **Fluxo da aplicação em Draw.io**, referenciado no README
- Toda a **documentação da disciplina concentrada neste README**

---

## 👥 Integrantes

| Integrante               | RM        |
|--------------------------|-----------|
| **Kaue Pastori Teixeira** | `98501` |
| **Felipe Bressane**       | `97688` |
| **Nicolas Boni**       | `551965` |

---

## 🎯 Tema e Contexto – Futuro do Trabalho

> **Pergunta norteadora:**  
> _Como preparar pessoas para carreiras que ainda nem existem, em um mundo de trabalho remoto, híbrido e profundamente impactado por IA?_

A **ORBITA Career API** é a camada de serviços de um “laboratório de carreiras do futuro”.  
Ela permite que empresas, escolas e hubs de inovação:

- Cadastrem **rotas de carreira** (CareerPaths) alinhadas ao Futuro do Trabalho;  
- Criem **missões gamificadas** (Missions) que simulam desafios reais dessas carreiras;  
- Acompanhem o **progresso dos usuários** (UserMissionProgress) ao longo da jornada.

Essa base de dados e APIs pode ser consumida, no futuro, por:

- Portais web e aplicativos mobile;  
- Plataformas de educação corporativa;  
- Assistentes de IA para orientação de carreira.

### Conexão com ODS

- **ODS 4 – Educação de Qualidade**  
  Trilhas de aprendizado contínuo e personalizável.

- **ODS 8 – Trabalho Decente e Crescimento Econômico**  
  Preparação para empregos de maior valor agregado em tecnologia.

- **ODS 9 – Indústria, Inovação e Infraestrutura**  
  Uso de APIs, dados e serviços digitais como infraestrutura de inovação em RH/educação.

- **ODS 10 – Redução das Desigualdades**  
  Acesso a trilhas baseadas em competências, não apenas em diploma ou networking.

---

## 🔧 Stack Técnica

- **Linguagem:** C# / .NET 8  
- **Framework Web:** ASP.NET Core Web API  
- **Banco de Dados:** SQL Server (LocalDB) via **Entity Framework Core**  
- **Documentação:** Swagger / OpenAPI (Swashbuckle)  
- **Versionamento de API:** `Microsoft.AspNetCore.Mvc.Versioning` (segmento na URL)  
- **Testes de integração manuais/automatizados:** Postman (collection incluída)  
- **Diagramas:** Draw.io (diagrams.net)

---

## 🗂 Estrutura da Solução

```text
Orbita.CareerApi/
├─ Controllers/
│  ├─ UsersController.cs
│  ├─ CareerPathsController.cs
│  ├─ MissionsController.cs
│  ├─ ProgressController.cs
│  └─ CareerPathsV2Controller.cs
├─ Models/
│  ├─ User.cs
│  ├─ CareerPath.cs
│  ├─ Mission.cs
│  └─ UserMissionProgress.cs
├─ Data/
│  └─ OrbitaContext.cs
├─ Properties/
│  └─ launchSettings.json
├─ appsettings.json
├─ Program.cs
└─ docs/
   ├─ fluxo-orbita-career-api.drawio
   └─ Orbita_CareerApi.postman_collection.json
```

> A pasta **`docs/`** concentra o fluxo da aplicação em Draw.io e a collection do Postman, conforme exigido na Global Solution.

---

## 📚 Funcionalidades da API

### Entidades do Domínio

- **User**
  - Guarda informações do participante (nome, e-mail, carga horária semanal disponível).

- **CareerPath**
  - Representa uma “rota de carreira do futuro” (ex.: Arquiteto de IA Ética).

- **Mission**
  - Missão gamificada associada a uma rota (projeto, estudo de caso, desafio prático etc).

- **UserMissionProgress**
  - Registro do progresso de um usuário em determinada missão (status, data de conclusão).

---

### Versionamento de API

- **v1** – Versão principal:
  - `/api/v1/Users`
  - `/api/v1/CareerPaths`
  - `/api/v1/Missions`
  - `/api/v1/Progress`

- **v2** – Exemplo de evolução:
  - `/api/v2/CareerPaths`  
    Endpoint de demonstração mostrando como a API pode evoluir sem quebrar clientes.

Configurado via:

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});
```

---

### 🌐 Principais Endpoints (v1)

Base: `https://localhost:7180/api/v1`

#### Users

| Verbo | Rota         | Descrição                      |
|------:|--------------|--------------------------------|
| GET   | `/Users`     | Lista todos os usuários        |
| GET   | `/Users/{id}`| Retorna usuário por Id         |
| POST  | `/Users`     | Cria novo usuário              |
| PUT   | `/Users/{id}`| Atualiza usuário existente     |
| DELETE| `/Users/{id}`| Remove usuário                 |

---

#### CareerPaths

| Verbo | Rota                           | Descrição                                      |
|------:|--------------------------------|------------------------------------------------|
| GET   | `/CareerPaths`                | Lista todas as rotas de carreira               |
| GET   | `/CareerPaths/{id}`           | Detalhes de uma rota específica                |
| POST  | `/CareerPaths`                | Cria nova rota de carreira                     |
| PUT   | `/CareerPaths/{id}`           | Atualiza rota existente                        |
| DELETE| `/CareerPaths/{id}`           | Remove rota                                    |
| GET   | `/CareerPaths/{id}/missions`  | Lista missões associadas à rota                |

---

#### Missions

| Verbo | Rota              | Descrição                                       |
|------:|-------------------|-------------------------------------------------|
| GET   | `/Missions`       | Lista todas as missões (filtro opcional por `careerPathId`) |
| GET   | `/Missions/{id}`  | Detalhes de uma missão                          |
| POST  | `/Missions`       | Cria nova missão                                |
| PUT   | `/Missions/{id}`  | Atualiza missão                                 |
| DELETE| `/Missions/{id}`  | Remove missão                                   |

---

#### Progress (UserMissionProgress)

| Verbo | Rota                         | Descrição                                 |
|------:|------------------------------|-------------------------------------------|
| GET   | `/Progress?userId={userId}`  | Lista progresso de um usuário             |
| GET   | `/Progress/{id}`             | Detalhes de um progresso específico       |
| POST  | `/Progress`                  | Cria registro de progresso (início da missão) |
| PUT   | `/Progress/{id}`             | Atualiza status do progresso (ex.: Concluída) |
| DELETE| `/Progress/{id}`             | Remove registro de progresso              |

---

## 🧠 Forma de Funcionamento (Visão de Negócio)

Fluxo típico dentro da plataforma ORBITA:

1. **Criar rota de carreira**  
   Um administrador registra uma `CareerPath` (ex.: “Especialista em Segurança de IA”).

2. **Cadastrar missões gamificadas**  
   Para cada rota, são cadastradas `Missions` com título, descrição, dificuldade, tempo estimado e recompensa de XP.

3. **Cadastrar usuários**  
   Alunos/colaboradores são cadastrados como `Users`, com nome, e-mail e horas semanais disponíveis.

4. **Iniciar missão**  
   Quando um usuário começa uma missão, a API cria um `UserMissionProgress` com `Status = "EmAndamento"`.

5. **Concluir missão**  
   Ao finalizar, o cliente envia um `PUT` para `/Progress/{id}` com `Status = "Concluída"` e `CompletedAt`.  
   A API registra essa conclusão e isso passa a compor o histórico de carreira do usuário.

6. **Consultas e relatórios**  
   A partir de `/Progress` e `/Missions`, é possível construir painéis que mostram:
   - quais missões do futuro do trabalho estão sendo concluídas;
   - quais trilhas são mais aderentes ao perfil do usuário.

---

## 🔀 Fluxo de Dados (Draw.io)

O **fluxo da aplicação** está implementado em Draw.io e incluído neste repositório em:

> `fluxo-orbita-career-api-profissional`

O diagrama modela o fluxo principal:

1. **Cliente** (Swagger, Postman ou front-end futuro) →  
2. **Endpoint HTTP** (ex.: `POST /api/v1/Progress`) →  
3. **Controller** correspondente (`ProgressController`) →  
4. **OrbitaContext (EF Core)** →  
5. **Banco SQL Server – OrbitaCareerDb** →  
6. **Resposta HTTP** com status + JSON retornando ao cliente.

---

## 📑 Documentação da API (Swagger)

A documentação da API é feita com **Swagger/OpenAPI**:

- Ao executar o projeto, a UI do Swagger fica acessível em:  
  `https://localhost:7180/` (porta conforme `launchSettings.json`).

Na interface do Swagger é possível:

- Visualizar todos os endpoints por versão (`v1` e `v2`);  
- Inspecionar modelos (`User`, `CareerPath`, `Mission`, `UserMissionProgress`);  
- Executar requisições de teste (GET, POST, PUT, DELETE);  
- Validar status codes e payloads.

Essa documentação cumpre o item: **“Documentação da API com Swagger”** solicitado na disciplina.

---

## ▶️ Como Executar a Aplicação

### 1) Pré-requisitos

- **.NET 8 SDK**  
- **SQL Server Express / LocalDB**  
- (Opcional) **Postman** instalado

### 2) Clonar o repositório

```bash
git clone https://github.com/KauePastori/orbita-career-api.git
cd orbita-career-api/Orbita.CareerApi
```

### 3) Configurar a connection string

Em `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=OrbitaCareerDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;"
}
```

Ajuste o servidor caso não esteja usando LocalDB.

### 4) Aplicar migrations (criar banco)

- Via Package Manager Console (Visual Studio):

```powershell
Update-Database
```

- Ou via CLI:

```bash
dotnet ef database update
```

### 5) Rodar a API

```bash
dotnet run
```

Abra o Swagger na URL exibida (por padrão `https://localhost:7180/`).

---

## 🧪 Testes com Postman

O repositório inclui uma **collection de testes** em:

> `docs/Orbita_CareerApi.postman_collection.json`

A collection executa automaticamente o fluxo:

1. Cria uma `CareerPath`;  
2. Lista `CareerPaths`;  
3. Cria uma `Mission` associada à rota;  
4. Lista `Missions` da rota;  
5. Cria um `User`;  
6. Cria um `UserMissionProgress` (início da missão);  
7. Atualiza o status para `Concluída`;  
8. Lista o progresso do usuário.

Importe no Postman:  
**`Import → File → selecionar Orbita_CareerApi.postman_collection.json`**.

---

## 🎥 Vídeo de Demonstração (máx. 5 minutos)

> **LINK DO VÍDEO:** https://youtu.be/7P6Wy4FySLw 
---
