using System.Text;
using GitHubSearchProject.DependencyInjection;
using GitHubSearchProject.Interfaces;
using GitHubSearchProject.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<IGitRepositoriesService, GitRepositoriesService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddHttpClient();
// Configure JWT Authentication
builder.Services.AddInfrastructureService(builder.Configuration);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", // Policy name
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Allow requests from this origin
                   .AllowAnyMethod() // Allow any HTTP method (GET, POST, PUT, DELETE, etc.)
                   .AllowAnyHeader(); // Allow any headers                  
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngularDevClient"); // Use the policy you defined
app.UseAuthorization();

app.MapControllers();

app.Run();
