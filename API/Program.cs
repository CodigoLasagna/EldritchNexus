using System.Text.Json.Serialization;
using Business.Contracts;
using Business.Implementation;
using Data.Contracts;
using Data.Implementation;

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

/*load services*/
services.AddScoped<IServerConfigService, ServerConfigService>();
services.AddScoped<IServerConfigRepository, ServerConfigRepository>();


var app = builder.Build();
//maybe put cors here
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
