
using System.Collections.Generic;

namespace API.Models;

public class AppDataContext
{
    public ISet<Aluno> Aluno { get; set; }
    public ISet<Imc> Imc { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
    }

}
