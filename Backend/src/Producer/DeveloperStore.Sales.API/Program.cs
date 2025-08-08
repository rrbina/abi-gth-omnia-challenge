using DeveloperStore.Sales.API.Extensions;
using DeveloperStore.Sales.API.Middlewares;
using DeveloperStore.Sales.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("https://localhost:7101", "http://localhost:5100");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomValidation();
builder.Services.AddCustomCors(builder.Environment);
builder.Services.AddFluentValidation();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddMediatRConfiguration();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddRebusConfiguration(builder.Configuration);

var app = builder.Build();
app.UseDeveloperStoreExceptionHandling();
app.UseDeveloperStorePipeline();
app.UseCustomCors();
app.UseAuthorization();
app.MapControllers();

SalesDbContextSeed.Seed(app.Services);

app.Run();

//https://localhost:7101/
