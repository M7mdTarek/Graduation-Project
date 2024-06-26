using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Test.Authentication;
using Test.Models;
using Test.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Test.Models.Repository;
using Test.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register HttpClient and ExternalApiService
builder.Services.AddHttpClient();
builder.Services.AddScoped<PredictDiseaseService>();
builder.Services.AddScoped<PredictSkinDiseaseService>();



builder.Services.AddScoped<SignupHandler>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<ForgetPasswordHandler>();
builder.Services.AddScoped<ValidateTokenHandler>();


builder.Services.AddScoped<ChronicDiseaseRepo>();
builder.Services.AddScoped<DrugRepo>();
builder.Services.AddScoped<SymptomRepo>();
builder.Services.AddScoped<UserDiseaseRepo>();
builder.Services.AddScoped<PostRepo>();


builder.Services.AddDbContext<AppDbContext>(
    builder => builder.UseSqlServer("server=.;database=MediSim;integrated security=true;trust server certificate=true")
    );

// for register options of jwt once
var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton(jwtOptions);

//for register options of mail once
var mailOptions = builder.Configuration.GetSection("Mail").Get<MailOptions>();
builder.Services.AddSingleton(mailOptions);

builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        //save the token string in auth prop 
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
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

app.UseAuthorization();

app.MapControllers();

app.Run();
