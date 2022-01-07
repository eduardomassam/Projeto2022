using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projeto2022.Migrations
{
    public partial class Migra30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(90)", nullable: false),
                    DasID = table.Column<string>(type: "varchar(10)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    defeitos = table.Column<string>(type: "varchar(1000)", nullable: false),
                    qualidades = table.Column<string>(type: "varchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Senioridade",
                columns: table => new
                {
                    SenioridadeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenioridadeName = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Senioridade", x => x.SenioridadeId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idLogin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(50)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", nullable: false),
                    controlaAcesso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idLogin);
                });

            migrationBuilder.CreateTable(
                name: "SkillsFuncionarios",
                columns: table => new
                {
                    idSkillFuncionario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tempExp = table.Column<string>(type: "varchar(2)", nullable: false),
                    observacoes = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false),
                    EmployeeID = table.Column<int>(type: "int", nullable: false),
                    SenioridadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillsFuncionarios", x => x.idSkillFuncionario);
                    table.ForeignKey(
                        name: "FK_SkillsFuncionarios_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsFuncionarios_Senioridade_SenioridadeId",
                        column: x => x.SenioridadeId,
                        principalTable: "Senioridade",
                        principalColumn: "SenioridadeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillsFuncionarios_Skills_SkillID",
                        column: x => x.SkillID,
                        principalTable: "Skills",
                        principalColumn: "SkillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DasID",
                table: "Employees",
                column: "DasID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillName",
                table: "Skills",
                column: "SkillName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SkillsFuncionarios_EmployeeID",
                table: "SkillsFuncionarios",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsFuncionarios_SenioridadeId",
                table: "SkillsFuncionarios",
                column: "SenioridadeId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillsFuncionarios_SkillID",
                table: "SkillsFuncionarios",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_usuario",
                table: "Usuarios",
                column: "usuario",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillsFuncionarios");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Senioridade");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
