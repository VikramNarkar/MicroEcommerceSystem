using InventoryService.Business;
using InventoryService.Business.BusinessAbstract;
using InventoryService.Data;
using InventoryService.Messaging;
using InventoryService.Repository;
using InventoryService.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepoInventoryService, RepoInventoryService>();
builder.Services.AddScoped<IInventoryBusinessService, InventoryBusinessService>();

builder.Services.AddHostedService<ProductCreatedConsumer>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<InventoryDbContext>(options => 
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
