using System;

namespace API.Models;

public class Aluno
{

public Aluno (){
    AlunoId = Guid.NewGuid().ToString();
}

    public string AlunoId { get; set; }
    public string AlunoName { get; set;}
    public string AlunoSobrenome{get;set;}
}
