using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Adicione servi�os ao cont�iner.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rotas de Viagem API", Version = "v1" });
});

// Inje��o de depend�ncia
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<ApplicationDb>(provider => new ApplicationDb(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rotas de Viagem API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
