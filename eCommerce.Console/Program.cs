using eCommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

class Program
{
    static async Task Main(string[] args)
    {
        // Obtém o caminho da raiz do projeto
        string projectRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(),
            @"../../../"));


        // onfigura o builder
        var builder = new ConfigurationBuilder()
            .SetBasePath(projectRoot)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        // Valida existência do arquivo
        if (File.Exists(Path.Combine(projectRoot, "appsettings.json")))
        {
            Console.WriteLine("Arquivo appsettings.json encontrado");
        }
        else
        {
            throw new FileNotFoundException(
                "Arquivo appsettings.json não encontrado",
                Path.Combine(projectRoot, "appsettings.json"));
        }

        var configuration = builder.Build();
        // Obtém a connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Valida connection string
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'DefaultConnection' não encontrada ou vazia");
        }
        else
        {
            Console.WriteLine("Connection string 'DefaultConnection' encontrada");
        }

        // Configura DbContext com retry policy
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString,
                sqlServerOptions => sqlServerOptions
                    .EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: new[] { 1205 }))
            .Options;

        await using var context = new ApplicationDbContext(options, configuration);

        try
        {
            Console.WriteLine("Iniciando migração do banco de dados...");

            // Criar banco de dados se não existir
            context.Database.EnsureCreated();

            // Executa migrações com timeout configurável
            var migrationTimeout = TimeSpan.FromMinutes(5);
            await context.Database.MigrateAsync(cancellationToken:
                new CancellationTokenSource(migrationTimeout).Token);

            bool bancoExiste = context.Database.CanConnect();
            if (!bancoExiste)
            {
                throw new Exception("Erro ao conectar ao banco de dados");
            }

            Console.WriteLine("Banco de dados conectado com sucesso!");

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"ERRO CRÍTICO: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();
            return;
        }
    }
}