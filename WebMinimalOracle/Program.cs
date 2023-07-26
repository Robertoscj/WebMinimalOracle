using Microsoft.EntityFrameworkCore;
using WebMinimalOracle.Contexto;
using WebMinimalOracle.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<Contexto>(options => options.UseOracle("stringConexao"));

var app = builder.Build();

app.UseSwagger();

app.MapPost("AdicionarProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});


app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produto = await contexto.Produto.FirstOrDefaultAsync(p=> p.Id == id);
    if(produto!= null)
    {
        contexto.Produto.Remove(produto);
        await contexto.SaveChangesAsync();
    }
    
});

app.MapGet("ListarProduto", async ( Contexto contexto) =>
{

    return await contexto.Produto.ToListAsync();  

});

app.MapGet("ObterProduto/{id}", async (int id ,Contexto contexto) =>
{

    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);

});


app.UseSwaggerUI();


app.Run();
