using eCommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

class Program
{
    static void Main(string[] args)
    {
        // Configuração da string de conexão
        string connectionString = "Server=localhost;Database=eCommerce;User ID=sa;Password=MinhaSenha123;";

        // Cria as opções do DbContext
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        // Cria uma instância do contexto
        using (var context = new ApplicationDbContext(options))
        {
            // Aplica todas as migrações pendentes
            context.Database.Migrate();

            // Verifica se o banco foi criado
            bool bancoExiste = context.Database.CanConnect();

            Console.WriteLine(bancoExiste
                ? "Banco de dados criado/atualizado com sucesso!"
                : "Erro ao conectar ao banco de dados");
        }
    }
}