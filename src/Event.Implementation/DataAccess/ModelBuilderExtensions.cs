using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event.Implementation.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            
            List<IdentityRole> roles = new List<IdentityRole>()
            {

                new IdentityRole{Name="Admin",NormalizedName="ADMIN"},
                new IdentityRole{Name="User",NormalizedName="USER"}
            };
             builder.Entity<IdentityRole>().HasData(roles);
     
        }
    }
}
