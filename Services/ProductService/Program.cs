using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Config;
using ProductService.Data;
using ProductService.Grpc;
using ProductService.Repository;
using ProductService.Repository.Abstract;
using Common.Messaging;
using ProductService.Business.BusinessAbstract;
using ProductService.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FakeProductService>();
builder.Services.AddScoped<IProductBusinessService, ProductBusinessService>();
builder.Services.AddScoped<IRepoProductService, RepoProductService>();

builder.Services.AddSingleton<IRabbitMQProducer, RabbitMQProducer>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ProductDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddGrpc();

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

app.MapGrpcService<ProductGrpcService>();

app.UseCors("AllowAll"); // Enables Angular frontend to make cross-origin API calls to this service

app.Run();
