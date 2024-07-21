using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JWT_token_auth_Demo.Data;
using System.Text;
using JWT_token_auth_Demo.AutoMigration;
using Microsoft.Data.SqlClient;
using System.Data;
using JWT_token_auth_Demo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using JWT_token_auth_Demo.StaticHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigurationManager _config = builder.Configuration;
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();  //enabling cors 
            });
        });

        builder.Services.AddScoped<IDbConnection>(c => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

        //For Database connection and AppDbContext is a class where we palce our models
        builder.Services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           );

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Adding JWTBearer & Authentication
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true, // validates the server that generate the token
                ValidateAudience = true,// validates the authorized receipent of generated token
                ValidateLifetime = true,
                ValidAudience = _config["JWT:ValidAudience"],
                ValidIssuer = _config["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]!))
            };
        });



        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
                });


        });
        #region Static AppConfig

        _config.GetSection(StaticGeneralAppConfig.AppSettings)
            .Bind(StaticGeneralAppConfig.Config);

        #endregion
        var app = builder.Build();
        try
        {
            // Ensure directories exist or create them
            string CaruoselImages = Path.Combine(Directory.GetCurrentDirectory(), "CaruoselImages");
            string SubCategoryThumbnailImages = Path.Combine(Directory.GetCurrentDirectory(), "SubCategoryThumbnailImages");
            string ProfileImages = Path.Combine(Directory.GetCurrentDirectory(), "ProfileImages");
            string CategoryThumbnailImages = Path.Combine(Directory.GetCurrentDirectory(), "CategoryThumbnailImages");
            string ProductThumbnail = Path.Combine(Directory.GetCurrentDirectory(), "ProductThumbnail");
            string AgentProfileImages = Path.Combine(Directory.GetCurrentDirectory(), "AgentProfileImages");

            if (!Directory.Exists(CaruoselImages))
            {
                Directory.CreateDirectory(CaruoselImages);
                Console.WriteLine($"Directory {CaruoselImages} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "CaruoselImages")),
                RequestPath = "/CaruoselImages"
            });

            if (!Directory.Exists(SubCategoryThumbnailImages))
            {
                Directory.CreateDirectory(SubCategoryThumbnailImages);
                Console.WriteLine($"Directory {SubCategoryThumbnailImages} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "SubCategoryThumbnailImages")),
                RequestPath = "/SubCategoryThumbnailImages"
            });

            if (!Directory.Exists(ProfileImages))
            {
                Directory.CreateDirectory(ProfileImages);
                Console.WriteLine($"Directory {ProfileImages} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ProfileImages")),
                RequestPath = "/ProfileImages"
            });

            if (!Directory.Exists(CategoryThumbnailImages))
            {
                Directory.CreateDirectory(CategoryThumbnailImages);
                Console.WriteLine($"Directory {CategoryThumbnailImages} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "CategoryThumbnailImages")),
                RequestPath = "/CategoryThumbnailImages"
            });

            if (!Directory.Exists(ProductThumbnail))
            {
                Directory.CreateDirectory(ProductThumbnail);
                Console.WriteLine($"Directory {ProductThumbnail} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ProductThumbnail")),
                RequestPath = "/ProductThumbnail"
            });

            if (!Directory.Exists(AgentProfileImages))
            {
                Directory.CreateDirectory(AgentProfileImages);
                Console.WriteLine($"Directory {AgentProfileImages} created.");
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AgentProfileImages")),
                RequestPath = "/AgentProfileImages"
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred {ex} while configuring static file providers.");
            throw;
        }


        app.UseCors("MyPolicy");


        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            Auto_Migration.Initialize(dbContext);
        }
        // Configure the HTTP request pipeline.
        /*if (app.Environment.IsDevelopment())
        {*/
        app.UseSwagger();
        app.UseSwaggerUI();
        //}

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

#region Static AppConfig

#endregion
