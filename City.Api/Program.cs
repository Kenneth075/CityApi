using City.Api;
using City.Api.DbContextCity;
using City.Api.Implementation;
using City.Api.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
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
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlcCommentsFilePath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlcCommentsFilePath);


    setupAction.AddSecurityDefinition("CityBearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access the API"
    });

    setupAction.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CityBearerAuth"
                }
            }, 
            
            new List<string>()
        }
    });
});

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

//settingup versioning

builder.Services.AddApiVersioning(setupAction =>
{
    setupAction.AssumeDefaultVersionWhenUnspecified = true;
    setupAction.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    setupAction.ReportApiVersions = true;
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
