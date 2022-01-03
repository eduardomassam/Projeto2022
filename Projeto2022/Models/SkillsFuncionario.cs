using Projeto2022.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Models
{
    /*
create table SkillsFuncionario(
idSkillFuncionario int primary key not null identity,
tempoExp int,
observacoes varchar(1000),
fk_idSkill integer not null,
fk_idFuncionario integer not null,
fk_idSenioridade integer not null,
foreign key (fk_idSkill) references skills(skillId),
foreign key (fk_idFuncionario) references funcionario(id),
foreign key (fk_idSenioridade) references Senioridade(SenioridadeId)
)
    */

    public class SkillsFuncionario
    {
        //Chave primeira idSkillFuncionario
        [Key]
        public int idSkillFuncionario { get; set; }

        //Coluna tempo de experiencia
        [Column(TypeName = "varchar(2)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Skill")]
        public string tempExp { get; set; }

        //Coluna observacoes
        [Column(TypeName = "nvarchar(1000)")]
        [DisplayName("Observacoes da Skill")]
        public string observacoes { get; set; }

        //Coluna FK de idSkill
        [Column(TypeName = "int")]
        [DisplayName("FK do id da tabela Skill")]
        [ForeignKey("SkillID")]
        public Skill fk_idSkill { get; set; }

        //Coluna FK de id Funcionario
        [Column(TypeName = "int")]
        [DisplayName("FK do id da tabela Funcionario")]
        [ForeignKey("EmployeeID")]
        public Employee fk_idFuncionario { get; set; }

        //Coluna FK de id Senioridade
        [Column(TypeName = "int")]
        [DisplayName("FK do id da tabela Senioridade")]
        [ForeignKey("SenioridadeId")]
        public Senioridade fk_idSenioridade { get; set; }


    }
}
