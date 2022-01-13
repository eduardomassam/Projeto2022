using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    [Authorize(Roles = "1,0")]

    public class EditarSkillFuncionarioModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SkillId { get; set; }


        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills WHERE idSkillFuncionario = '{Id}' and SkillID = '{SkillId}'";

            SqlDataReader reader = cmd.ExecuteReader();


            if (await reader.ReadAsync())
            {
                Nome = reader.GetString(1);
            }
            await conexao.CloseAsync();
        }

        public void OnPost()
        {
           
        }


        public async Task<IActionResult> OnGetApagar()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();

            cmd.CommandText = $"DELETE FROM SkillsFuncionarios WHERE EmployeeID = {Id} and SkillID = {SkillId}";

            await cmd.ExecuteReaderAsync();
          
            return new JsonResult(new { removidoSkillFuncionario = "Tecnologia Removida com sucesso", teste=Id });
        }
    }
}
