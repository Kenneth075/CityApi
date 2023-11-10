using City.Api;
using City.Api.DbContextCity;
using City.Api.Implementation;
using City.Api.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("log/cityInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//builder.Logging.ClearProviders();  //Access to the logger. Clear all log.
//builder.Logging.AddConsole();  //Manually log

// Add services to the container.

builder.Host.UseSerilog();  //Telling aspnetcore to use serilog instead of the build-in logger.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Using compiler directive.
#if DEBUG
builder.Services.AddTransient<ILocalMailService, LocalMailService>();
#else
builder.Services.AddTransient<ILocalMailService, CloudMailService>();
#endif

builder.Services.AddSingleton<FileExtensionContentTypeProvider>(); //Allows us to inject content-type provider to all the part our code for file uploading.
builder.Services.AddSingleton<CitiesDtoStore> ();

builder.Services.AddDbContext<DbCityContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:CityContext"]));

builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//setting up jwt token bearer
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});

//setting up authorization policy
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("MostBeFromLagos", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("city", "Lagos");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(Endpoint =>
{
    Endpoint.MapControllers();
});
//app.MapControllers();

app.Run();
