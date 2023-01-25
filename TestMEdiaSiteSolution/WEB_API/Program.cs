using CORE.DataSeed;
using CORE.DbContexts;
using Microsoft.EntityFrameworkCore;
using REPOSITORY.Abstractions;
using REPOSITORY.Implementations;

var builder = WebApplication.CreateBuilder(args);


// ADD DB CONTEXT
// ============================================================================
builder.Services.AddDbContext<MediaContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// ADD DEPENDENCIES
// ============================================================================
builder.Services.AddTransient<IMediaTypeRepository, MediaTypeRepository>();
builder.Services.AddTransient<IMediaRepository, MediaRepository>();
builder.Services.AddTransient<IWatchListRepository, WatchListRepository>();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy", policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        }
    );
});



var app = builder.Build();




// ADD MIGRATION
// ============================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<MediaContext>();

        // DATABASE SCHEMA MIGRATION
        await context.Database.MigrateAsync();

        // DATABASE DATA SEEDER
        await DataSeeder.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "DETECTED ERROR DURING MIGRATION");
    }
}






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseAuthorization();

app.MapControllers();

app.Run();
