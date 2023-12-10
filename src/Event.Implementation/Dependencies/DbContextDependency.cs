using Event.Application.Interfaces;
using Event.Implementation.ImplementRepositories;

namespace Event.Implementation.Dependencies;
public static class DbContextDependency
{
      
        public static IServiceCollection AddDbServices(this IServiceCollection services,
                                                        IConfiguration configuration)
        {


        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("EventDbConnection"));
                
            });
        services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
        services.AddScoped(typeof(IAuthService),typeof(AuthService));


              return services;
        }
    }
