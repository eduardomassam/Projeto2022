using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class EditarSkillModel : PageModel
    {
   
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }


        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills WHERE SkillID = '{Id}'";

            SqlDataReader reader = cmd.ExecuteReader();
         
      
            if (await reader.ReadAsync())
            {
                Nome = reader.GetString(1);             
            }
            await conexao.CloseAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            if (String.IsNullOrEmpty(Nome))
            {
                await conexao.CloseAsync();
                throw new Exception("Error");
                return new JsonResult(new { Msg = "Campo deve ser preenchido" });
            }

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"UPDATE Skills SET SkillName = '{Nome}' WHERE SkillID = {Id}";
           

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Skill Editada com sucesso" });

        }

        public async Task<IActionResult> OnGetApagar()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"DELETE FROM Skills WHERE SkillID = {Id}";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Skill Removida com sucesso" });
        }
    }
}
