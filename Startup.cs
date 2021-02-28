using AoTTG2.IDS.AutoMapper;
using AoTTG2.IDS.Data;
using AoTTG2.IDS.Data.Repositories;
using AoTTG2.IDS.Data.Repositories.Interfaces;
using AoTTG2.IDS.Filters;
using AoTTG2.IDS.Models;
using AoTTG2.IDS.Security;
using AoTTG2.IDS.Services;
using AoTTG2.IDS.Services.Interfaces;
using AoTTG2.IDS.Validation;
using Discord.OAuth2;
using Duende.IdentityServer;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AoTTG2.IDS
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(x => x.AddProfile(typeof(CustomProfile)));
            services.AddControllersWithViews(options => { options.Filters.Add<ExceptionHandler>(); })
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReportValidator>());

            // Setup Persistence
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, o => o.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Setup IDS
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v5/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<ApplicationUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 443;
            });

            var oAuthConfig = Configuration.GetSection("OAuth");

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    // register your IdentityServer with Google at https://console.developers.google.com
                    // enable the Google+ API
                    // set the redirect URI to https://localhost:5001/signin-google
                    options.ClientId = oAuthConfig.GetSection("Google")["ClientId"];
                    options.ClientSecret = oAuthConfig.GetSection("Google")["ClientSecret"];
                })
                .AddDiscord(options =>
                {
                    options.AppId = oAuthConfig.GetSection("Discord")["ClientId"];
                    options.AppSecret = oAuthConfig.GetSection("Discord")["ClientSecret"];
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.Prompt = DiscordOptions.PromptTypes.Consent;
                    options.Scope.Add("identify");
                })
                .AddVkontakte(options =>
                {
                    options.ClientId = oAuthConfig.GetSection("Vkontakte")["ClientId"];
                    options.ClientSecret = oAuthConfig.GetSection("Vkontakte")["ClientSecret"];
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                })
                .AddCertificate(opt => { opt.AllowedCertificateTypes = CertificateTypes.SelfSigned; });

            // By default IDS will always redirect to the login page. For API endpoints this should be 401
            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/AoTTG2/API") &&
                            context.Response.StatusCode == StatusCodes.Status200OK)
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.CompletedTask;
                        }
                        else
                        {
                            context.Response.Redirect(context.RedirectUri);
                            return Task.CompletedTask;
                        }
                    }
                };
            });

            services.AddHealthChecks();
            services.AddRazorPages();

            // Repositories
            services.AddScoped<IReportRepository, ReportRepository>();

            // Services
            services.AddScoped<IReportService, ReportService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
                var forwardOptions = new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                    RequireHeaderSymmetry = false
                };

                forwardOptions.KnownNetworks.Clear();
                forwardOptions.KnownProxies.Clear();

                // ref: https://github.com/aspnet/Docs/issues/2384
                app.UseForwardedHeaders(forwardOptions);
            }


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add(
                    "Content-Security-Policy",
                    "default-src 'self'; " +
                    " script-src 'self' ajax.googleapis.com ajax.aspnetcdn.com; " +
                    "style-src 'self'; " +
                    "img-src 'self'");

                await next();
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapHealthChecks("/health");
            });
            app.UseHttpsRedirection();

            UpdateDatabase(app);
            InitializeDatabase(app);
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.Clients)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.IdentityResources)
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in Config.ApiScopes)
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                foreach (var role in Roles.GetConfigRoles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        _ = roleManager.CreateAsync(new IdentityRole(role)).Result;
                    }
                }
            }
        }
    }
}