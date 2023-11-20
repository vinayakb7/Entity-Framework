using Microsoft.EntityFrameworkCore;
using WebApiWithMSSQL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDBSQL")));
builder.Services.AddDbContext<DBContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("StudentDBMySQL"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("StudentDB"))));
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

app.UseAuthorization();

app.MapControllers();

app.Run();
