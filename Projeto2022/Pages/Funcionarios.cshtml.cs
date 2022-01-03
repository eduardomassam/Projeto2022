using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Projeto2022.Pages
{
    public class FuncionariosModel : PageModel
    {
        public List<FuncionarioViewModel> Funcionarios { get; set; }
        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Employees";

            SqlDataReader reader = cmd.ExecuteReader();

            //Iniciando a lista como empty
            Funcionarios = new List<FuncionarioViewModel>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Funcionarios.Add(new FuncionarioViewModel
                {
                    Id = reader.GetInt32(0),
                    Nome = reader.GetString(1),
                    DasID = reader.GetString(2),
                    Email = reader.GetString(3),
                    Defeitos = reader.GetString(4),
                    Qualidades = reader.GetString(5),
                });
            }
            await conexao.CloseAsync();
        }

        public class FuncionarioViewModel
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string DasID { get; set; }
            public string Email { get; set; }
            public string Qualidades { get; set; }
            public string Defeitos { get; set; }

        }
    }
}
