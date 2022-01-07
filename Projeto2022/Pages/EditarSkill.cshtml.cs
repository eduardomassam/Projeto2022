using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Projeto.Models;
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
        public List<Skill> Skills { get; set; }


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

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills";

            SqlDataReader reader = cmd.ExecuteReader();


            if (String.IsNullOrEmpty(Nome))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }
        
            //Iniciando a lista como empty
            Skills = new List<Skill>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Skills.Add(new Skill
                {
                    SkillID = reader.GetInt32(0),
                    SkillName = reader.GetString(1),
                });

            }

            foreach (var Skill in Skills)
            {

                var isExisteTec = Skill.SkillName.ToUpper().Equals(Nome.ToUpper());
                if (isExisteTec)
                {
                    await conexao.CloseAsync();
                    return new JsonResult(new { repetido = "Tecnologia já existe, portanto nada foi cadastrado" });
                }

            }
            await conexao.CloseAsync();
            await conexao.OpenAsync();

        
            cmd.CommandText = $"UPDATE Skills SET SkillName = '{Nome}' WHERE SkillID = {Id}";
            await cmd.ExecuteReaderAsync();
            await conexao.CloseAsync();

            
            return new JsonResult(new { skilleditada = "Skill Editada com sucesso" });
        }

     
        public async Task<IActionResult> OnGetApagar()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"DELETE FROM Skills WHERE SkillID = {Id}";

            await cmd.ExecuteReaderAsync();      

            return new JsonResult(new { removido = "Tecnologia Removida com sucesso" });
        }
    }
}
