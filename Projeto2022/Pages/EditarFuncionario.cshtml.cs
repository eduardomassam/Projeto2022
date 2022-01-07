using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class EditarFuncionarioModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o DAS ID")]
        [BindProperty(SupportsGet = true)]
        public string DasID { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Defeitos { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Qualidades { get; set; }

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
                DasID = reader.GetString(2);
                Email = reader.GetString(3);
                Defeitos = reader.GetString(4);
                Qualidades = reader.GetString(5);


            }
            await conexao.CloseAsync();
        }

        public async Task<IActionResult> OnPost()
        {

            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();

            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(DasID))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }

            cmd.CommandText = $"UPDATE Employees SET FullName = '{Nome}', DasID = '{DasID}', email = '{Email}', defeitos = '{Defeitos}', qualidades = '{Qualidades}' WHERE EmployeeID = {Id}";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { funcionarioeditado = "Funcionário Editado com sucesso" });

        }

        public async Task<IActionResult> OnGetApagar()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"DELETE FROM Employees WHERE EmployeeID = {Id}";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { funcionarioremovido = "Funcionário Removido com sucesso" });
        }
    }
}
