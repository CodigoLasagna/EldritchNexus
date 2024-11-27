using System.Text.Json.Serialization;
using Business.Contracts;
using Business.Implementation;
using Data.Contracts;
using Data.Implementation;
using EldritchNexus.SignalR;
using FastSurvey.Controllers;
using Helper.lib2git;
using Microsoft.AspNetCore.SignalR;
using Model;

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
services.AddSingleton<IClientService, ClientService>();
services.AddScoped<IClientRepository, ClientRespository>();
services.AddSingleton<IRoleService, RoleService>();

services.AddScoped<IUserService, UserService>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IOrganizationService, OrganizationService>();
services.AddScoped<IOrganizationRepository, OrganizationRepository>();


var app = builder.Build();

Console.WriteLine("node type: ");
var role = Console.ReadLine()?.Trim().ToLower();
var roleService = app.Services.GetRequiredService<IRoleService>();

if (role == "server")
{
    Console.WriteLine("Starting server...");
    //app.UseCors("AllowSpecificOrigin");
    app.UseCors();
    app.MapHub<CentralHub>("/centralHub");

    using (var scope = app.Services.CreateScope())
    {
        var organizationService = scope.ServiceProvider.GetRequiredService<IOrganizationService>();
        OrganizationCreateModel newOrg = new OrganizationCreateModel();
        newOrg.Name = "MainOrganization";
        int orgResponse = organizationService.CreateOrganization(newOrg);
        if (orgResponse == 0)
            Console.WriteLine("organization already exists");
        if (orgResponse > 0)
            Console.WriteLine("base organization created");
        
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        bool response = userService.AddGenericSysAdmin();
        if (response)
            Console.WriteLine("base system admin created");
        if (!response)
            Console.WriteLine("System admin already exists");
    }
    
    string repostPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"GitNexus/ServerRepos"
    );
    Console.WriteLine(repostPath);
    GitServer server = new GitServer(repostPath);
    server.CreateRepository("DummyServer");
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
