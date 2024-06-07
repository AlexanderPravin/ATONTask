using System.Text;
using Application;
using Domain.Entities;
using Domain.Enum;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Insert Json Web Token",
            Name = "Swagger authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                Array.Empty<string> ()
            }
        });
    }
);

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateIssuer = true,

            ValidateLifetime = true,

            ValidateAudience = false,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<Context>();
    
    context.Database.Migrate();

    if (context.Users.IsNullOrEmpty())
        context.Users.Add(new User
        {
            Id = Guid.NewGuid(),
            Login = "Admin",
            Password = "Admin",
            CreatedBy = "Admin",
            Gender = Gender.Male,
            IsAdmin = true,
            Name = "Admin",
            CreatedOn = DateTime.UtcNow
        });

    context.SaveChanges();
}

app.UseSwagger(); 

app.UseSwaggerUI();

app.MapControllers();   

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Run();