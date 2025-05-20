using Application;
using Infrastructure;
using Infrastructure.Data;
using Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    await app.InitialiseDatabaseAsync();
else
    app.UseHsts();

app.UseOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHealthChecks("/health");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseExceptionHandler(options => { });


app.UseAuthorization();

app.MapControllers();

app.Run();