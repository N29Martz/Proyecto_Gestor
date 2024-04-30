using gestor_archivos_backend.DataBase;
using gestor_archivos_backend.Entities;
using gestor_archivos_backend.Services;
using gestor_archivos_backend.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace gestor_archivos_backend
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //esto es la conexion a la base de datos
            services.AddDbContext<GestorDbContext>(options =>
                //es variable lo que hace es acceder a la configuracion de la base de datos appsettings.json
                options.UseOracle(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(opt =>
                {
                    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    });

                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
                });

            //custom services
            services.AddTransient<IArchivosService, ArchivosService>();
            services.AddTransient<IFoldersService, FoldersService>();
            services.AddTransient<IUsuariosService, UsuariosService>();
            services.AddTransient<IRolesService, RolesService>();

            //Auth service
            services.AddTransient<IAuthService, AuthService>();

            //google service
            

            //add automapper service
            services.AddAutoMapper(typeof(Startup));

            //esto es para que se pueda acceder a la peticion http
            services.AddHttpContextAccessor();

            //add identity
            services.AddIdentity<UsuarioEntity, RolEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<GestorDbContext>()
            .AddDefaultTokenProviders();


            // Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });


            //darle permisos a la api para que pueda ser consumida por el frontend
            services.AddCors(options =>
            {
               options.AddDefaultPolicy(builder =>
               {
                   builder.WithOrigins(Configuration["FrontendURL"])
                   .AllowAnyHeader()
                   .AllowAnyMethod();
               });
            });

            // services.AddControllers().AddJsonOptions(x =>
            //     x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // esto es para que se pueda acceder a la api desde cualquier origen
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //para rutas mas rapidas
            app.UseRouting();

            //para usar cors
            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
