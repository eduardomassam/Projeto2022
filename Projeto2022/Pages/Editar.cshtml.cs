using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class EditarModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool Remember { get; set; }
        public int ControlaAcesso { get; set; }

        public async Task OnGet()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Usuarios WHERE idLogin = '{Id}'";

            SqlDataReader reader = cmd.ExecuteReader();
            ControlaAcesso = 0;
            if (Remember)
            {
                ControlaAcesso = 1;
            }

            if (await reader.ReadAsync())
            {
                
                Usuario = reader.GetString(1);
                Nome = reader.GetString(3);
                Email = reader.GetString(4);
                ControlaAcesso = reader.GetInt32(5);              
            }
              await conexao.CloseAsync();
        }

        public async Task<IActionResult> OnPost()
        {
            ControlaAcesso = 0;
            if (Remember)
            {
                ControlaAcesso = 1;
            }
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();

            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(Usuario))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }

            cmd.CommandText = $"UPDATE Usuarios SET usuario = '{Usuario}', nome = '{Nome}', email = '{Email}', controlaAcesso = {ControlaAcesso} WHERE idLogin = {Id}";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { usereditado = "Usuário Editado com sucesso" });

        }

        public async Task<IActionResult> OnGetApagar()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"DELETE FROM Usuarios WHERE idLogin = {Id}";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { userremovido = "Usuário Removido com sucesso" });
        }
    }
}
