using Microsoft.EntityFrameworkCore;
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
create table usuario(
idLogin int primary key not null identity,
usuario varchar(50) not null UNIQUE,
senha nvarchar(1000) not null,
nome varchar(50) not null,
email varchar(100) UNIQUE,
isAcessoConfig BIT not null
)
 
  */
    public class Usuario
    {
        //Chave primaria idLogin
        [Key]
        public int idLogin { get; set; }

        //Coluna usuario   
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Usuario")]

        public string usuario { get; set; }

        //Coluna senha
        [Column(TypeName = "nvarchar(1000)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Senha")]
        public string senha { get; set; }

        //Coluna nome
        [Column(TypeName = "varchar(50)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Nome")]
        public string nome { get; set; }

        //Coluna email
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Email")]
        public string email { get; set; }

        //Coluna controla skill (pra nao ter duplicidade de mesma skill com senioridade diferente)
        [Column(TypeName = "int")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Controlador da Skill")]
        public int controlaSkill { get; set; }
    }
}
