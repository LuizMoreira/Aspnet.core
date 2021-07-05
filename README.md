# Asp.net core, CQRS, EF core

# Implemementação envolvendo as melhores práticas adotadas para criação de webapi. 

O objetivo desse código é criar uma API REST ful para a utilização de uma simples enquete. Apesar do escopo ser pequeno, o desenvolvimento utiliza as melhores práticas para desenvolviemnto de webapi utilizadas atualmente. 

*Ainda não foi criado um projeto e banco separados para serem replicados os dados para leitura da query repository. O contexto de query está comparitlhando o mesmo dbcontext de escrita* 

## Requisitos

O banco de dados utilizado para a aplicação é o SQL Server 2012 ou superior;

Servidor de aplicação com .net core framework 3.1:
  - Recomendado e testado no Windows 2012 server ou superior;

## Procedimentos

Procedimento de configuração do banco de dados Sql server:
  - Criar o banco de dados de nome Poll;
  - Criar um usuário: enquete e senha: enquete. Com permissão db_owner;
  - Verificar o ip e a porta de acesso para incluir na string de conexão da aplicação (por padrão tcp e porta 1433);
  - Verificar se o ip do servidor de aplicação está com acesso ao servidor de banco de dados;
  - obs: *O sistema está utilizando microsoft migrate EF code first para criação e atualização automática do schema do banco de dados.*
  
Procedimento para iniciar a aplicação:
  - Abrir o projeto que foi enviado por email com o nome * LuizFernandoDesafioEnquete *;
  - Abrir o arquivo appsettings.json dentro do projeto PollContext.webapi:
    - Informar a string de conexão com o BD;
    - Instalar o dotnet-ef:
      - dotnet tool install --global dotnet-ef;
    - Iniciar o migration no projeto de infra: 
      - dotnet ef migrations add InitialCreate --startup-project ..\PollContext.webapi\
    - Update database: 
      - dotnet ef database update --startup-project ..\PollContext.webapi\
  - Abrir o diretório do projeto: *PollContext.webapi* pelo powershell e executar a seguinte linha de comando:
    - *dotnet publish --configuration Release*
    - copiar o conteúdo dessa pasta: *PollContext.webapi\bin\Release\netcoreapp2.2\publish* para o endereço criado no IIS para a aplicação;

## Testes

Para acesso as rotas da api, foi criada uma página swagger padrão apenas para facilitar o teste, sem domentação apropriada;
  - http://ip/swagger/index.html
