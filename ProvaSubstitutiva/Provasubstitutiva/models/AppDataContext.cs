using Microsoft.EntityFrameworkCore;

namespace ProvaSubstitutiva.Models;


public class AppDataContext : DbContext
{
    public DbSet<Alunos> Alunos { get; set; }
    public DbSet<Imc> Imc { get; set; }
   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
    }

}
