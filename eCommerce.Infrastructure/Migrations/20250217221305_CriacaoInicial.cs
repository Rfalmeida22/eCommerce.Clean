using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eCommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brokers",
                columns: table => new
                {
                    IdBroker = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NmBroker = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers", x => x.IdBroker);
                });

            migrationBuilder.CreateTable(
                name: "Brokers_Varejistas",
                columns: table => new
                {
                    IdSequencial = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBroker = table.Column<int>(type: "int", nullable: false),
                    IdVarejista = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brokers_Varejistas", x => x.IdSequencial);
                });

            migrationBuilder.CreateTable(
                name: "Historicos",
                columns: table => new
                {
                    Historicos_Cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Historicos_Aca = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Historicos_Dat = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 30, 17, 25, 10, 457, DateTimeKind.Local).AddTicks(4796)),
                    Historicos_Det = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Historicos_Tab = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    Usuarios_Cod = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historicos", x => x.Historicos_Cod);
                });

            migrationBuilder.CreateTable(
                name: "LogImportacaoVarejoDetalhe",
                columns: table => new
                {
                    IdDetalhe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broker = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CdCartao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CpfComprador = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataCancelamento = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataCriacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataValidade = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DataVenda = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdLog = table.Column<int>(type: "int", nullable: false),
                    Loja = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Varejista = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Vendedor = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogImportacaoVarejoDetalhe", x => x.IdDetalhe);
                });

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    IdLoja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CdCnpj = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CdLoja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdLojista = table.Column<int>(type: "int", nullable: false),
                    IdVarejista = table.Column<int>(type: "int", nullable: false),
                    NmLoja = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TxEndereco = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.IdLoja);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Usuarios_Cod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBroker = table.Column<int>(type: "int", nullable: false),
                    IdLoja = table.Column<int>(type: "int", nullable: false),
                    IdVarejista = table.Column<int>(type: "int", nullable: false),
                    SenhaAnterior = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Usuarios_Ati = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Usuarios_DatCad = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 10, 30, 17, 25, 10, 612, DateTimeKind.Local).AddTicks(1808)),
                    Usuarios_Ema = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Usuarios_EmpPad = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Usuarios_Nom = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Usuarios_Sen = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Usuarios_VisAutcom = table.Column<bool>(type: "bit", nullable: false),
                    Usuarios_VisAutemp = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Usuarios_Cod);
                });

            migrationBuilder.CreateTable(
                name: "Varejistas",
                columns: table => new
                {
                    IdVarejista = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CdBanner = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CdCorFundo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CdVarejista = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NmVarejista = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TxLinkSite = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Varejistas", x => x.IdVarejista);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brokers");

            migrationBuilder.DropTable(
                name: "Brokers_Varejistas");

            migrationBuilder.DropTable(
                name: "Historicos");

            migrationBuilder.DropTable(
                name: "LogImportacaoVarejoDetalhe");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Varejistas");
        }
    }
}
