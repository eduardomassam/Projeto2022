using Microsoft.EntityFrameworkCore;
using Projeto.Models;

namespace Projeto2022
{
    public class Contexto : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<SkillsFuncionario> SkillsFuncionarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;initial Catalog=mySkill;User ID=usuario;password=senha;language=Portuguese;Trusted_Connection=True;");
         
        }

    }
}
