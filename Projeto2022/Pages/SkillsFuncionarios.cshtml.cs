using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Projeto2022.Pages
{
    [Authorize(Roles = "1,0")]

    public class SkillsFuncionariosModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SkillId { get; set; }
        [BindProperty(SupportsGet = true)]
        public int EmployeeId { get; set; }


        public List<SkillsFuncionariosViewModel> SkillsFuncionarios { get; set; }

        public async Task OnGet()
        {

            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");

            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"Select SkillsFuncionarios.* , Skills.*, Employees.EmployeeID, Employees.FullName From SkillsFuncionarios Inner join Skills on Skills.SkillID = SkillsFuncionarios.SkillID inner join Employees on Employees.EmployeeID = SkillsFuncionarios.EmployeeID";

            SqlDataReader reader = cmd.ExecuteReader();

            //Iniciando a lista como empty
            SkillsFuncionarios = new List<SkillsFuncionariosViewModel>();
           
            
            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                
                    SkillsFuncionarios.Add(new SkillsFuncionariosViewModel
                    {
                        idSkillFuncionario = reader.GetInt32(0),
                        tempExp = reader.GetString(1),
                        observacoes = reader.GetString(2),
                        SkillID = reader.GetInt32(3),
                        EmployeeID = reader.GetInt32(4),
                        SenioridadeId = reader.GetInt32(5),
                        SkillIDFK = reader.GetInt32(6),
                        SkillNameFK = reader.GetString(7),
                        EmployeeIDFK = reader.GetInt32(8),
                        NameFK = reader.GetString(9),

                    });
                
             
                
            }
            await conexao.CloseAsync();
        }

        public class SkillsFuncionariosViewModel
        {
            public int idSkillFuncionario { get; set; }
            public string tempExp { get; set; }
            public string observacoes { get; set; }
            public int SkillID { get; set; }
            public int EmployeeID { get; set; }
            public int SenioridadeId { get; set; }
            public int SkillIDFK { get; set; }
            public string SkillNameFK { get; set; }
            public int EmployeeIDFK { get; set; }
            public string NameFK { get; set; }




        }
    }
}
