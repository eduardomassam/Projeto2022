using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Projeto2022.Pages
{
    public class SkillsModel : PageModel
    {

        public List<SkillViewModel> Skills { get; set; }
        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills";

            SqlDataReader reader = cmd.ExecuteReader();

            //Iniciando a lista como empty
            Skills = new List<SkillViewModel>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Skills.Add(new SkillViewModel
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                });
            }
            await conexao.CloseAsync();
        }

        public class SkillViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
        }
 
    }
}
