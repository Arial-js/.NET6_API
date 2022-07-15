using EsercitazioneAPI.Database;
using EsercitazioneAPI.Repository;
using EsercitazioneAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using EsercitazioneAPI.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using EsercitazioneAPI.Models;
using EsercitazioneAPI.Models.DTO;
using EsercitazioneAPI.Service;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordEncrypter, PasswordEncrypter>();
builder.Services.AddScoped<IServiceLayer, UserServiceLayer>();
builder.Services.AddDbContext<ApiDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<LoginDTO, UsersModel>()
        .ForMember(d => d.Name, opt => opt.Ignore())
        .ForMember(d => d.LastName, opt => opt.Ignore())
        .ForMember(d => d.Id, opt => opt.Ignore());
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<UserWithoutIdDTO, UsersModel>()
        .ForMember(d => d.Id, opt => opt.Ignore());
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<UserWithoutRoleAndIdDTO, UsersModel>()
        .ForMember(d => d.Id, opt => opt.Ignore())
        .ForMember(d => d.Role, opt => opt.Ignore());
});

builder.Services.AddSwaggerGen(option =>
{
    //Adding Swagger Authorize
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme.",
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //Adding Validation for the Token
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Configuration["JWT:Issuer"],
        ValidAudience = Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
    };
});

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
