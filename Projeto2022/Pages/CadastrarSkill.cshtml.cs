using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Projeto.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Projeto2022.Pages
{

    [Authorize(Roles = "1")]
    public class CadastrarSkillModel : PageModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }
        public List<Skill> Skills { get; set; }
        public List<SelectListItem> Skills1 { get; set; }




        public async Task OnGet()
        {

         
            SqlConnection conexao2 = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha;");
            await conexao2.OpenAsync();

            SqlCommand cmd2 = conexao2.CreateCommand();

            cmd2.CommandText = $"SELECT * from Skills";

            SqlDataReader reader2 = cmd2.ExecuteReader();

            


            Skills1 = new List<SelectListItem>();

            //Enquanto tiver algo para ser lido
            while (await reader2.ReadAsync())
            {
                Skills1.Add(new SelectListItem
                {
                    Value = reader2.GetInt32(0).ToString(),
                    Text = reader2.GetString(1),
                });

            }
            

            TempData["MySkills"] = Skills1;

            await conexao2.CloseAsync();


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
                return new JsonResult(new { vazio = "Verifique os campos obrigatórios" });
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
                    return new JsonResult(new { repetido = "Técnologia já existe, portanto nada foi cadastrado" });
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
