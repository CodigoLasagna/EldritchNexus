using System.Text.Json.Serialization;
using Business.Contracts;
using Business.Implementation;
using Data.Contracts;
using Data.Implementation;
using EldritchNexus.SignalR;
using FastSurvey.Controllers;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var config = builder.Configuration;

/*remove when prod and fix to prod*/
services.AddCors(options =>
{
    options.AddDefaultPolicy(corsPolicyBuilder => corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod());
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddSignalR();

/*load services*/
services.AddScoped<IServerConfigService, ServerConfigService>();
services.AddScoped<IServerConfigRepository, ServerConfigRepository>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<IClientRepository, ClientRespository>();
services.AddSingleton<IRoleService, RoleService>();


var app = builder.Build();

Console.WriteLine("node type: ");
var role = Console.ReadLine()?.Trim().ToLower();
var roleService = app.Services.GetRequiredService<IRoleService>();

if (role == "server")
{
    Console.WriteLine("Starting server...");
    app.UseCors("AllowSpecificOrigin");
    app.MapHub<CentralHub>("/centralHub");
}
else if (role == "client")
{
    Console.WriteLine("Starting client...");
    app.UseCors();

}
roleService.Role = role;


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
