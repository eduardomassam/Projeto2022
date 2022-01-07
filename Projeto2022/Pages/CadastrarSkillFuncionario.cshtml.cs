using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class CadastrarSkillFuncionarioModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o tempo de Experiência")]
        [BindProperty(SupportsGet = true)]
        public string tempExp { get; set; }


        [BindProperty(SupportsGet = true)]
        public string observacoes { get; set; }

        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Employees WHERE EmployeeID = '{Id}'";

            SqlDataReader reader = cmd.ExecuteReader();


            if (await reader.ReadAsync())
            {
                Nome = reader.GetString(1);
            }
            await conexao.CloseAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();


            SqlCommand cmd = conexao.CreateCommand();

            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(tempExp))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }

            //cmd.CommandText = $"INSERT INTO Employees (tempExp,observacoes,SkillID,EmployeeID,SenioridadeId) VALUES ('{tempExp}' , '{observacoes}' , '{SkillID}' , '{EmployeeID}' , '{SenioridadeId}')";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { funcionarioskill = "Skill cadastrada com sucesso nesse Funcionário" });

        }
    }
}
