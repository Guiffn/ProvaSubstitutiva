using System;
namespace API.Models;

public class Alunos{

public Alunos (){
    AlunoId = Guid.NewGuid().ToString();
}

    public string AlunoId { get; set; }
    public string AlunoName { get; set;}
    public string AlunoSobrenome{get;set;}
    
}
