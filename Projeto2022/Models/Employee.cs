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
create table funcionario(
id int primary key not null identity,
dasID int not null UNIQUE,
nome varchar(90) not null,
email varchar(100) UNIQUE,
defeitos nvarchar(1000),
qualidades nvarchar(1000),
)
    */
    [Index(nameof(Employee.DasID),IsUnique = true)]
    public class Employee
    {
        
        //PK do funcionario
        [Key]
        public int EmployeeID { get; set; }

        //Coluna nome do funcionário
        [Column(TypeName = "varchar(90)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Nome")]
        public string FullName { get; set; }

        //Coluna DAS ID do funcionário (login da ATOS se chama DAS ID)
        //Padrão do DAS ID é a letra a seguida de 6 números que cresce conforme contratações
        //Exemplo a630510 é contratado, o próximo contratado terá DAS ID a630511
        [Column(TypeName = "varchar(10)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Das ID")]
        public string DasID { get; set; }

        //Coluna email do funcionario
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Email")]
        public string email { get; set; }

        //coluna defeitos
        [Column(TypeName = "varchar(1000)")]
        [DisplayName("Defeitos")]
        public string defeitos { get; set; }


        //coluna qualidades
        [Column(TypeName = "varchar(1000)")]
        [DisplayName("Qualidades")]
        public string qualidades { get; set; }

    }
}
