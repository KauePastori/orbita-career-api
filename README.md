# ORBITA Career API – Global Solution 2º Semestre

API RESTful em ASP.NET Core que faz parte do projeto **ORBITA – Laboratório de Carreiras do Futuro**, desenvolvida para o Global Solution do 2º semestre na disciplina **Software Development C#**.

A solução permite **cadastrar rotas de carreira do futuro, suas missões gamificadas e o progresso dos usuários**, ajudando pessoas e empresas a se prepararem para o **Futuro do Trabalho**.

## Como rodar

1. Certifique-se de ter o .NET 8 SDK instalado.
2. Configure a connection string do SQL Server no arquivo `appsettings.json` (chave `DefaultConnection`).
3. Rode as migrations (após instalar o EF Core Tools, se necessário):

```bash
dotnet ef database update
```

4. Execute a API:

```bash
dotnet run
```

5. Acesse a documentação Swagger em:

- `https://localhost:7180/swagger` 
