using Persistence;
using Core.CrossCuttingConcerns.Exceptions;
using Application;
using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("admin");
    });

    options.AddPolicy("CustomerOnly", policy =>
    {
        policy.RequireRole("customer");
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
       opt =>
       {
           opt.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = true,
               ValidateIssuer = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,
               ValidIssuer = builder.Configuration["Token:Issuer"],
               ValidAudience = builder.Configuration["Token:Audience"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
               ClockSkew = TimeSpan.Zero
           };
       });
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
EncryptionService.Initialize(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureCustomExceptionMiddleware();
app.UseAuthentication();
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
