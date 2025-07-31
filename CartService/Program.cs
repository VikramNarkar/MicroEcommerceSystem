using CartService.Services;
using Contracts.Protos;
using CartService.Services.Abstract;
using CartService.Business;
using CartService.Repository.Abstract;
using CartService.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<ICartService, FakeCartService>();
builder.Services.AddScoped<IRepoCartService, RepoCartService>();
builder.Services.AddScoped<CartBusinessService>();

builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(o =>
{
    o.Address = new Uri("https://localhost:7002"); // Replace with ProductService's actual HTTPS URL
});


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

app.Run();
