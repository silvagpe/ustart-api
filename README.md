# Api - Projeto UStart

## Projeto inicial

Segue o código fonte do projeto inicial do curso
https://github.com/silvagpe/ustart-api-inicial


## EF Core

Instalar a ferramenta do EF no CLI
```
dotnet tool install --global dotnet-ef
```
https://docs.microsoft.com/pt-br/ef/core/cli/dotnet

### Migrations

Antes de criar a migrations é necessário adicionar a referência do para a biblioteca do EF Design
```bash
cd Infrastructure 
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.2

cd API
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.2

dotnet clean && dotnet build
```

Como criar as migrations
```bash
cd API
dotnet ef migrations add usuarios -c UStartContext --project ../Infrastructure/Infrastructure.csproj

dotnet ef migrations add usuarios_nome -c UStartContext --project ../Infrastructure/Infrastructure.csproj

#Criar grupo produto
dotnet ef migrations add grupo_produto -c UStartContext --project ../Infrastructure/Infrastructure.csproj

#Criar clientes
cd API
dotnet ef migrations add cliente -c UStartContext --project ../Infrastructure/Infrastructure.csproj

#Criar produto
cd API
dotnet ef migrations add produto -c UStartContext --project ../Infrastructure/Infrastructure.csproj


```


## Configuração do banco de dados

```
Database=_BANCO_;
Username=_USUARIO_;
Password=_SENHA_;
Host=_HOST_;
Port=5432;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;
```


## Postman

Link
https://www.postman.com/grey-satellite-490926/workspace/ustart-postman-public

