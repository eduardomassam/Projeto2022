using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Projeto.Models;
using System.ComponentModel.DataAnnotations;
using static Projeto2022.Pages.SkillsModel;

namespace Projeto2022.Pages
{
    [Authorize(Roles = "1,0")]
    public class CadastrarSkillFuncionarioModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o Nome")]
        [BindProperty(SupportsGet = true)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigratório informar o tempo de Experiência")]
        [BindProperty(SupportsGet = true)]
        public string tempExp { get; set; }


        [BindProperty(SupportsGet = true)]
        public string observacoes { get; set; }

        [BindProperty(SupportsGet = true)]
        public int EmployeeId { get; set; }

        public List<SelectListItem> IDNOVO { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Senioridade { get; set; }
        public List<SkillsFuncionario> SkillNoFuncionario { get; set; }







        async Task RecebeSkillId()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();
            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Skills";
            SqlDataReader reader = cmd.ExecuteReader();
            //Iniciando a lista como empty
            Skills = new List<SelectListItem>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Skills.Add(new SelectListItem
                {
                    Value = reader.GetInt32(0).ToString(),
                    Text = reader.GetString(1),
                });
            }

            TempData["MySkills"] = Skills;
            await conexao.CloseAsync();
        }
        async Task RecebeSenioridadeId()
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();
            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Senioridade";
            SqlDataReader reader = cmd.ExecuteReader();
            //Iniciando a lista como empty
            Senioridade = new List<SelectListItem>();

            //Enquanto tiver algo para ser lido
            while (await reader.ReadAsync())
            {
                Senioridade.Add(new SelectListItem
                {
                    Value = reader.GetInt32(0).ToString(),
                    Text = reader.GetString(1),
                });
            }

            TempData["Senioridade"] = Senioridade;
            await conexao.CloseAsync();
        }

        //async Task RecebeIdFuncionario()
        //{
        //    SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
        //    await conexao.OpenAsync();
        //    SqlCommand cmd = conexao.CreateCommand();
        //    cmd.CommandText = $"SELECT * from Employees where EmployeeID = {Id}";
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    //Iniciando a lista como empty
        //    IDNOVO = new List<SelectListItem>();

        //    //Enquanto tiver algo para ser lido
        //    while (await reader.ReadAsync())
        //    {
        //        IDNOVO.Add(new SelectListItem
        //        {
        //            Value = reader.GetInt32(0).ToString(),
        //            Text = reader.GetString(1),
        //        });
        //    }
        //    TempData["IDNOVO"] = IDNOVO;



        //    await conexao.CloseAsync();

        //}

        async Task<bool> RecebeIdFuncionario(string MySkills)
        {
            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();
            SqlCommand cmd = conexao.CreateCommand();
            int t=0;
            cmd.CommandText = $"SELECT * from SkillsFuncionarios where EmployeeID = {Id} and SkillID = {MySkills}";
            SqlDataReader reader = cmd.ExecuteReader();
            if (await reader.ReadAsync())
            {               
                t = reader.GetInt32(0);
            }
            if(t>=1)
            {
                return true;
            }
            return false;
        }

        public async Task OnGet()
        {
            await RecebeSkillId();
            await RecebeSenioridadeId();
            //await RecebeIdFuncionario();
        }



        public async Task<IActionResult> OnPostAsync(string MySkills, string Senioridade, string IDNOVO)
        {


            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();
            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from SkillsFuncionarios";

            SqlDataReader reader = cmd.ExecuteReader();


            


            if (String.IsNullOrEmpty(tempExp))
            {
                await conexao.CloseAsync();
                return new JsonResult(new { vazio = "Campo deve ser preenchido" });
            }

            //Iniciando a lista como empty
            SkillNoFuncionario = new List<SkillsFuncionario>();

            //Enquanto tiver algo para ser lido
            //while (await reader.ReadAsync())
            //{
            //    SkillNoFuncionario.Add(new SkillsFuncionario
            //    {
            //        idSkillFuncionario = reader.GetInt32(0),
            //        tempExp = reader.GetString(1),
            //        observacoes = reader.GetString(2),
            //        SkillID = reader.GetInt32(3),
            //        EmployeeID = reader.GetInt32(4),
            //        SenioridadeId = reader.GetInt32(5),
            //        SkillIDFK = reader.GetInt32(6),
            //        SkillNameFK = reader.GetString(7),
            //        EmployeeIDFK = reader.GetInt32(8),
            //        NameFK = reader.GetString(9),

            //    });



            //foreach (var Skill in SkillNoFuncionario)
            //{

            //    var isExisteID = Id.Equals(IDNOVO);
            //    var isExisteTec = Skill.Equals(Skill.fk_idSkill);

            //    if (isExisteTec && isExisteID)
            //    {
            //        await conexao.CloseAsync();
            //        return new JsonResult(new { repetido = "Técnologia já existe nesse funcionário, portanto nada foi cadastrado" });
            //    }

            //}

            await conexao.CloseAsync();
            await conexao.OpenAsync();

            var variavel = await RecebeIdFuncionario(MySkills);
            if (variavel)
            {
                return new JsonResult(new { funcrepetido = "Tecnologia já existe nesse funcionario" });

            }
            else
            {
                cmd.CommandText = $"INSERT INTO SkillsFuncionarios (tempExp,observacoes,SkillID,EmployeeID,SenioridadeId) VALUES ('{tempExp}' , '{observacoes}' , '{MySkills}' , '{Id}' , '{Senioridade}')";
                await cmd.ExecuteReaderAsync();
                await conexao.CloseAsync();
                return new JsonResult(new { cadastrofuncionarioskill = "Skill cadastrada com sucesso nesse Funcionário" });

            }



            return new JsonResult(new { errodecadastro = "Erro desconhecido, favor verificar todos os campos" });

        }



    }
}


