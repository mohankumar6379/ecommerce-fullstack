
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
var cs=builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StoreContext>(x=>x.UseSqlite(cs));
builder.Services.AddScoped<IProductRepository,ProductRepository>();

var app = builder.Build();

using(var scope=app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    var loggerFactory=services.GetRequiredService<ILoggerFactory>();
    try{
        var context=services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await StoreContextSeed.SeedAsync(context,loggerFactory); 
    }
    catch(Exception ex)
    {
        var logger=loggerFactory.CreateLogger<Program>();
        logger.LogError(ex,"An error occured during migration");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthorization();

app.Run();
