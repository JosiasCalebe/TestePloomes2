using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace TesteAPI
{
    public class Startup : IDisposable
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen();

            services.AddSingleton<IConfiguration>(
                                  _ => Configuration);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JwtSettings")["Secret"])),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            var info = new OpenApiInfo()
            {
                Title = "TesteAPI",
                Version = "v1"
            };
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Usuario", info);
                c.SwaggerDoc("Cliente", info);


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Header de autorização JWT usando o esquema Bearer (Exemplo: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        new string[]{}
                    }
                });
            });

            services.AddSwaggerGenNewtonsoftSupport();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnablePersistAuthorization();
                c.SwaggerEndpoint("/swagger/Usuario/swagger.json", "Usuario");
                c.SwaggerEndpoint("/swagger/Cliente/swagger.json", "Cliente");




                c.DocumentTitle = "API - Documentação - Swagger";
            });

            app.UseRouting();

            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
