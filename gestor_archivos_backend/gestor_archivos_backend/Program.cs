using CloudinaryDotNet;
using dotenv.net;
using gestor_archivos_backend;
using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

//hay que extraer del metodo constructor
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, builder.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {

        var userManager = services.GetRequiredService<UserManager<UsuarioEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<RolEntity>>();

        await GestorDbSeeder.LoadDataAsync(userManager, roleManager, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "ocurrio un error");
    }
}

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

app.Run();
