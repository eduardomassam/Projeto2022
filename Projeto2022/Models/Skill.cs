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
create table skills(
skillId int primary key not null identity,
nomeSkill varchar(50) not null,
controlaSkill int not null
)   
    */
    public class Skill
    {
        //Chave primeira skillId
        [Key]
        public int SkillID { get; set; }

        //Coluna nome da Skill
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Nome da Skill")]
        public string SkillName { get; set; }

        //Coluna controla skill (pra nao ter duplicidade de mesma skill com senioridade diferente)
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Controlador da Skill")]
        public int controlaSkill { get; set; }

    }
}
