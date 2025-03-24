using AutoMapper;
using Bank.Repository;
using Bank.Repository.DTOs;
using Bank.Repository.Entities;
using Bank.Repository.Entities.Services;
using Bank.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// AutoMapper 
builder.Services.AddAutoMapper(typeof(UserProfile)); // Register the UserProfile class
builder.Services.AddAutoMapper(typeof(CustomerProfile));
builder.Services.AddAutoMapper(typeof(LoanProfile));
builder.Services.AddAutoMapper(typeof(TransactionProfile));



//Här säger vi att vi skall arbeta med authentication via JWT

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   //Här säger vi hur vi skall jobba med JWT
   .AddJwtBearer(opt => {
       opt.TokenValidationParameters = new TokenValidationParameters
       {
           //Issuer är vem (vilken server) som utfärdat en JWT token
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = "http://localhost:5290",
           ValidAudience = "http://localhost:5290",
           IssuerSigningKey =
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mykey1234567&%%485734579453%&//1255362"))
       };
   });

//a Swagger generator that goes through all endpoints and 
//documents these in a JSON file.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank API", Version = "v1" });

    // Add JWT Authentication configuration
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and the JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
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
            Array.Empty<string>()
        }
    });
});


// Add dependency injection
builder.Services.AddScoped<IBank, BankContext>();
builder.Services.AddScoped<IUserRepository, UserRepo>();
builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<IAdminRepository, AdminService>();
builder.Services.AddScoped<AdminService>();

builder.Services.AddScoped<UserRepo>(); // Or services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<CustomerService>(); // Or its interface if it has one

builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddScoped<TransactionService>();







var app = builder.Build();

//Here we tell the application to use Swagger + SwaggerUI
app.UseSwagger();
app.UseSwaggerUI();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank API v1");
});

// use routing
app.UseRouting();


//Detta måste komma efter att routng satts upp
//Stöd för inloggning = authentication
//och behörighet= authorization
app.UseAuthentication();
app.UseAuthorization();

// Map endpoints
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


app.Run();
