using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Projeto.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Projeto2022.Pages
{

    [Authorize(Roles = "1")]
    public class CadastrarSkillModel : PageModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo de � de preechimento obrigat�rio")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }
        public List<Skill> Skills { get; set; }

      

        public void OnGet()
        {
 
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha;");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills";

            SqlDataReader reader = cmd.ExecuteReader();

            if (String.IsNullOrEmpty(Nome))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Verifique os campos obrigat�rios" });
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
                    return new JsonResult(new { repetido = "T�cnologia j� existe, portanto nada foi cadastrado" });
                }

            }     

            await conexao.CloseAsync();
            await conexao.OpenAsync();

            cmd.CommandText = $"INSERT INTO Skills (SkillName) VALUES ('{Nome}')";
            await cmd.ExecuteReaderAsync();

            await conexao.CloseAsync();

            return new JsonResult(new { Msg = "Skill cadastrada com sucesso" });

        }

    }
}
