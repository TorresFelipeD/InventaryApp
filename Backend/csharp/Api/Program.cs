using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Database.Context.InventaryAppDbContext>(op => op.UseInMemoryDatabase(databaseName: "InventaryAppDB"));

var app = builder.Build();

app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Database.Context.InventaryAppDbContext>();
     dbContext.Database.EnsureCreated();
};

app.Run();
