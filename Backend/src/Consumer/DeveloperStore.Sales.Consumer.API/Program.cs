using DeveloperStore.Sales.API.Extensions;
using DeveloperStore.Sales.Consumer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("http://localhost:5000");
}

// Configurações de CORS
builder.Services.AddCustomCors(builder.Environment);

// Configuração do Rebus
builder.Services.AddRebusConfiguration(builder.Configuration);

// Configuração do MongoDB
builder.Services.AddMongoContext(builder.Configuration);
builder.Services.AddMongoRepositories();
builder.Services.AddMongoServices();
builder.Services.AddMongoHandlers();

// Configuração da serialização JSON
builder.Services.ConfigureJsonOptions();

var app = builder.Build();

// Middlewares
app.UseCustomCors();

// Configurações do pipeline de requisições
app.UseRouting(); // Garante que as rotas sejam configuradas
app.MapControllers(); // Mapeia os controladores da aplicação

app.UseLifetimeOnStartup();

app.Run();