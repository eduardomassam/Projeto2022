using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Projeto2022.Pages
{
    [Authorize(Roles = "1")]

    public class UsuariosModel : PageModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; }
        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Usuarios";

            SqlDataReader reader = cmd.ExecuteReader();

            //Iniciando a lista como empty
            Usuarios = new List<UsuarioViewModel>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Usuarios.Add(new UsuarioViewModel
                {
                    Id = reader.GetInt32(0),
                    Usuario = reader.GetString(1),
                    Nome = reader.GetString(3),
                    Email = reader.GetString(4),
                    Admin = reader.GetInt32(5)
                });
            }
            await conexao.CloseAsync();
        }

        public class UsuarioViewModel
        {
            public int Id { get; set; }
            public string Usuario { get; set; }  
            public string Nome { get; set; }
            public string Email { get; set; }
            public int Admin { get; set; }  
        }
    }
}
