using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto2022.Models
{
    /*
create table Senioridade(
SenioridadeId int primary key not null identity,
SenioridadeName varchar(20) not null
)
*/
    public class Senioridade
    {
        //Chave primeira SenioridadeId
        [Key]
        public int SenioridadeId { get; set; }

        //Coluna senioridade
        [Column(TypeName = "varchar(20)")]
        [Required(ErrorMessage = "Esse campo de é de preechimento obrigatório")]
        [DisplayName("Nome da Senioridade")]
        public string SenioridadeName { get; set; }

    }
}