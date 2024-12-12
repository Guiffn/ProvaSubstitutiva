

using Microsoft.EntityFrameworkCore;
using ProvaSubstitutiva.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total",
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();



app.MapGet("/", () => "Prova A1");



//ENDPOINTS DE ALUNO

// POST: http://localhost:5000/api/aluno/cadastrar

app.MapPost("/api/aluno/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Alunos aluno) =>

{
  ctx.Alunos.Add(aluno);

  ctx.SaveChanges();

  return Results.Created("", aluno);

});



//ENDPOINTS DE IMC

// POST: http://localhost:5000/api/imc/cadastrar

app.MapPost("/api/imc/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Imc imc) =>

{
  imc.Resultado = imc.Peso / (imc.Peso * imc.Altura); // Cálculo do IMC
  ctx.Imcs.Add(imc);
  ctx.SaveChanges();
  return Results.Created("", imc);

});



// GET: http://localhost:5000/api/imc/listar

app.MapGet("/api/imc/listar", ([FromServices] AppDataContext ctx) =>

{
  if (ctx.Imcs.Any())
  {
    return Results.Ok(ctx.Imcs.Include(i => i.Aluno).ToList());
  }

  return Results.NotFound("Nenhum IMC encontrado");

});



// GET: http://localhost:5000/api/imc/listarporaluno/{alunoId}

app.MapGet("/api/imc/listarporaluno/{alunoId}", ([FromServices] AppDataContext ctx, int alunoId) =>

{
  var result = ctx.Imcs.Where(i => i.AlunoId == alunoId).ToList();

  if (result.Any())
    return Results.Ok(result);
  return Results.NotFound($"Nenhum IMC encontrado para o aluno com ID {alunoId}.");

});



// PUT: http://localhost:5000/api/imc/alterar/{id}

app.MapPut("/api/imc/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] int id, [FromBody] Imc updatedImc) =>

{
  var imcExistente = ctx.Imcs.Find(id);
  if (imcExistente == null)
  {

    return Results.NotFound($"IMC com ID {id} não encontrado.");

  }
  imcExistente.Peso = updatedImc.Peso;

  imcExistente.Altura = updatedImc.Altura;

  imcExistente.Resultado = updatedImc.Peso / (updatedImc.Altura * updatedImc.Altura); // Recalcular IMC

  ctx.SaveChanges();

  return Results.Ok(imcExistente);

});



app.UseCors("Acesso Total");

app.Run();

