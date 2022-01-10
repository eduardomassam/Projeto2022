using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    [Authorize(Roles = "1")]

    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "É obrigratório informar a Senha")]
        [DataType(DataType.Password)]
        [BindProperty(SupportsGet = true)]
        public string Senha { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool Remember { get; set; }
        public int ControlaAcesso { get; set; }
        

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();
            
            ControlaAcesso = 0;
            if(Remember)
            {
                ControlaAcesso = 1;
            }
         

            SqlCommand cmd = conexao.CreateCommand();

            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(Usuario) || Senha == null)
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }

            cmd.CommandText = $"INSERT INTO Usuarios (usuario,senha,nome,email,controlaAcesso) VALUES ('{Usuario}' , '{Senha}' , '{Nome}' , '{Email}' , '{ControlaAcesso}')";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Usuário cadastrado com sucesso" });

        }


    }
}
