using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage ="É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage ="É obrigratório informar o Usuário")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "É obrigratório informar a Senha")]
        [DataType(DataType.Password)]
        [BindProperty(SupportsGet = true)]
        public string Senha { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [Required(ErrorMessage = "É obrigratório informar o se é Admin ou Não")]
        [BindProperty(SupportsGet = true)]
        public int ControlaAcesso { get; set; }

        public void OnGet()
        {

        }

        public async Task <IActionResult> OnPostAsync()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"INSERT * from Usuarios (usuario,senha,nome,email,controlaAcesso) VALUES ('{Usuario}'), '{Senha}','{Nome}','{Email}','{ControlaAcesso}'";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Usuário cadastrado com sucesso" });

        }


    }
}
