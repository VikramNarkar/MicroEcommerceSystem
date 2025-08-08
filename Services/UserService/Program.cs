using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Repository;
using UserService.Repository.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FakeUserService>();
builder.Services.AddScoped<IRepoUserService, RepoUserService>();

builder.Services.AddDbContext<UserDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});                                     // For Angular

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll"); // Enables Angular frontend to make cross-origin API calls to this service

app.Run();
