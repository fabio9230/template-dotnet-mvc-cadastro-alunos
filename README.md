# Cadastro de Alunos

Aplicação web de exemplo desenvolvida em **C# com ASP.NET Core MVC e .NET 8**, implementando um CRUD completo para cadastro de alunos.

O projeto utiliza **Entity Framework Core com banco de dados em memória (InMemory)**, tornando a aplicação simples de executar e adequada para estudos, demonstrações e prototipação.

## Tecnologias

* .NET 8
* C#
* ASP.NET Core MVC
* Entity Framework Core 8
* Entity Framework Core InMemory
* Razor Views
* Bootstrap 5
* Data Annotations

## Funcionalidades

O sistema possui as seguintes operações:

* Listar alunos
* Cadastrar aluno
* Visualizar detalhes de um aluno
* Editar aluno
* Excluir aluno
* Pesquisar alunos por:

  * Nome
  * CPF
  * E-mail
* Ativar/inativar aluno
* Validação dos campos obrigatórios
* Validação de formato de e-mail
* Validação de CPF duplicado

## Dados do Aluno

Cada aluno possui os seguintes dados:

| Campo              | Tipo     | Obrigatório |
| ------------------ | -------- | ----------- |
| Id                 | Guid     | Sim         |
| Status             | Boolean  | Sim         |
| Nome               | String   | Sim         |
| Data de Nascimento | DateTime | Sim         |
| CPF                | String   | Sim         |
| E-mail             | String   | Sim         |
| Telefone           | String   | Não         |

## Estrutura do Projeto

```text
CadastroAlunos/
│
├── Controllers/
│   └── AlunosController.cs
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── Models/
│   └── Aluno.cs
│
├── Views/
│   ├── Alunos/
│   │   ├── Create.cshtml
│   │   ├── Delete.cshtml
│   │   ├── Details.cshtml
│   │   ├── Edit.cshtml
│   │   └── Index.cshtml
│   │
│   ├── Shared/
│   │   └── _Layout.cshtml
│   │
│   ├── _ViewImports.cshtml
│   └── _ViewStart.cshtml
│
├── appsettings.json
├── Program.cs
├── CadastroAlunos.csproj
└── README.md
```

## Pré-requisitos

Para executar o projeto é necessário ter instalado:

* .NET 8 SDK

Verifique a instalação com:

```bash
dotnet --version
```

O comando deve retornar uma versão compatível com o .NET 8.

## Instalação

Clone o projeto:

```bash
git clone <URL_DO_REPOSITORIO>
```

Entre na pasta:

```bash
cd CadastroAlunos
```

Restaure as dependências:

```bash
dotnet restore
```

## Banco de Dados

O projeto utiliza o provedor:

```text
Microsoft.EntityFrameworkCore.InMemory
```

O banco é configurado no `Program.cs`:

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CadastroAlunosDb"));
```

Não é necessário instalar ou configurar:

* SQL Server
* MySQL
* PostgreSQL
* Docker
* Connection String
* Migration

O banco é criado automaticamente em memória quando a aplicação é executada.

### Importante

Como o banco é **em memória**, os dados não são persistidos.

Ao encerrar e iniciar novamente a aplicação, os alunos cadastrados anteriormente serão perdidos.

Essa abordagem é utilizada neste projeto para facilitar o aprendizado e a execução do exemplo.

Para uma aplicação real, recomenda-se substituir o InMemory por um banco de dados persistente, como SQL Server ou PostgreSQL.

## Executando o projeto

Execute:

```bash
dotnet run
```

A aplicação será iniciada e o terminal exibirá a URL de acesso.

Exemplo:

```text
Now listening on: https://localhost:7000
```

Acesse a aplicação pelo navegador:

```text
https://localhost:7000/Alunos
```

A rota padrão da aplicação já está configurada para abrir o cadastro de alunos:

```text
/Alunos
```

## CRUD

### Listagem

A tela inicial apresenta todos os alunos cadastrados.

É possível pesquisar por:

* Nome
* CPF
* E-mail

Também estão disponíveis as ações:

* Detalhes
* Editar
* Excluir

### Cadastro

Na tela **Novo Aluno**, é possível informar:

```text
Nome
CPF
Data de Nascimento
E-mail
Telefone
Status
```

O `Id` é gerado automaticamente utilizando `Guid`.

### Edição

Através da opção **Editar**, os dados do aluno podem ser alterados.

O `Id` do aluno é preservado durante a alteração.

### Detalhes

A tela de detalhes apresenta todas as informações cadastradas para o aluno.

### Exclusão

A aplicação solicita confirmação antes de excluir um aluno.

## Modelo

O modelo `Aluno` utiliza Data Annotations para validação:

```csharp
public class Aluno
{
    [Key]
    public Guid Id { get; set; }

    [Display(Name = "Ativo")]
    public bool Status { get; set; } = true;

    [Required]
    [StringLength(150)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    [Required]
    [StringLength(14)]
    public string CPF { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [StringLength(20)]
    public string Telefone { get; set; } = string.Empty;
}
```

## Arquitetura

O projeto utiliza o padrão **MVC (Model-View-Controller)**.

### Model

Responsável pela representação dos dados:

```text
Models/Aluno.cs
```

### View

Responsável pela interface HTML/Razor:

```text
Views/Alunos/
```

Contém:

```text
Index.cshtml
Create.cshtml
Edit.cshtml
Details.cshtml
Delete.cshtml
```

### Controller

Responsável pelo fluxo das requisições e operações CRUD:

```text
Controllers/AlunosController.cs
```

## Entity Framework Core

O acesso aos dados é realizado através do `ApplicationDbContext`:

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
}
```

As operações de consulta utilizam Entity Framework Core:

```csharp
var alunos = await _context.Alunos
    .OrderBy(a => a.Nome)
    .ToListAsync();
```

## Validações

O projeto possui algumas validações básicas:

### Nome

* Obrigatório
* Máxim
