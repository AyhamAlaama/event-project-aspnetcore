using Event.Domain;
using Event.Domain.IdentityModels.ExtendedUser;

namespace Event.Implementation.DataAccess;
    public class ApplicationDbContext
    :
    IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext
        (DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }
    public DbSet<Event.Domain.Events> Events { get; set; }
       

    

    protected override void OnModelCreating(ModelBuilder builder)
    {

        //AspNetRoles - AspNetUsers - AspNetRoleClaims
        //AspNetUserClaims - AspNetUserLogins - AspNetUserRoles - AspNetUserTokens

        base.OnModelCreating(builder);
        builder.HasDefaultSchema("event");
        builder.Entity<ApplicationUser>().ToTable("Users","Security");
        builder.Entity<IdentityRole>().ToTable("Roles", "Security");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim","Security");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "Security");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken", "Security");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "Security");
        builder.Seed();
    }
}