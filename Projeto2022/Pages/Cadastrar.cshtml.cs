using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class CadastrarModel : PageModel
    {
        [Required(ErrorMessage ="� obrigrat�rio informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage ="� obrigrat�rio informar o Usu�rio")]
        [BindProperty(SupportsGet = true)]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "� obrigrat�rio informar a Senha")]
        [DataType(DataType.Password)]
        [BindProperty(SupportsGet = true)]
        public string Senha { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }


        [Required(ErrorMessage = "� obrigrat�rio informar o se � Admin ou N�o")]
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

            return new JsonResult(new { Msg = "Usu�rio cadastrado com sucesso" });

        }


    }
}
