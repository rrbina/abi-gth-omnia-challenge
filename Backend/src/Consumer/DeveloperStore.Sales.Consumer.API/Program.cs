using DeveloperStore.Sales.API.Extensions;
using DeveloperStore.Sales.Consumer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("http://localhost:5000");
}

// Configura��es de CORS
builder.Services.AddCustomCors(builder.Environment);

// Configura��o do Rebus
builder.Services.AddRebusConfiguration(builder.Configuration);

// Configura��o do MongoDB
builder.Services.AddMongoContext(builder.Configuration);
builder.Services.AddMongoRepositories();
builder.Services.AddMongoServices();
builder.Services.AddMongoHandlers();

// Configura��o da serializa��o JSON
builder.Services.ConfigureJsonOptions();

var app = builder.Build();

// Middlewares
app.UseCustomCors();

// Configura��es do pipeline de requisi��es
app.UseRouting(); // Garante que as rotas sejam configuradas
app.MapControllers(); // Mapeia os controladores da aplica��o

app.UseLifetimeOnStartup();

app.Run();