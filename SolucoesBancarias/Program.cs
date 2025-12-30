using SolucoesBancarias.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<SolucoesBancarias.Service.IServicosBancarios, SolucoesBancarias.Service.ServicosBancarios>();
builder.Services.AddScoped<SolucoesBancarias.Repositories.IRepositorioDeContas, SolucoesBancarias.Repositories.RepositorioDeContas>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await app.RunAsync();
