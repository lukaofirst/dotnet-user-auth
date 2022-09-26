using Core.Settings;
using IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<SettingsConfig>(builder.Configuration.GetSection("SettingsConfig"));
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

var appSettings = builder.Configuration.AddJsonFile("appsettings.json").Build();
DependencyContainer.StartServices(builder.Services, appSettings);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
