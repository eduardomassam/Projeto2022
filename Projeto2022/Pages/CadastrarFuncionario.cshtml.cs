using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    public class CadastrarFuncionarioModel : PageModel
    {
        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o DAS ID")]
        [BindProperty(SupportsGet = true)]
        public string DasID { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Qualidades { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Defeitos { get; set; }



        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();


            SqlCommand cmd = conexao.CreateCommand();

            if (String.IsNullOrEmpty(Nome) || String.IsNullOrEmpty(DasID))
            {
                await conexao.CloseAsync();
                throw new Exception("Error");
                return new JsonResult(new { Msg = "Campo deve ser preenchido" });
            }

            cmd.CommandText = $"INSERT INTO Employees (FullName,DasID,email,defeitos,qualidades) VALUES ('{Nome}' , '{DasID}' , '{Email}' , '{Defeitos}' , '{Qualidades}')";

            await cmd.ExecuteReaderAsync();

            return new JsonResult(new { Msg = "Funcionário cadastrado com sucesso" });

        }
    }
}
