using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Egharpay.Models.Identity
{
    /// <summary>
    /// Identity models that haven't changed in presence of permission extension.
    /// </summary>

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("EmailConfirmed", this.EmailConfirmed.ToString()));
            return userIdentity;
        }

        public string Name { get; set; }
        public int? PersonnelId { get; set; }
       
    }

    #region Permissions May possible moved
    [Table("AspNetPermissions")]
    public class ApplicationPermission
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    [Table("AspNetUserPermissions")]
    public class ApplicationUserPermission
    {
        [Column(Order = 0), Key]
        public string UserId { get; set; }

        [Column(Order = 2), Key]
        public string PermissionId { get; set; }
    }

    [Table("AspNetRolePermissions")]
    public class ApplicationRolePermission
    {
        [Column(Order = 0), Key]
        public string RoleId { get; set; }

        [Column(Order = 2), Key]
        public string PermissionId { get; set; }
    }
    #endregion

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name)
            : base(name)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString(), throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        static ApplicationDbContext()
        {
            //Database.SetInitializer<ApplicationDbContext>(null);
            Database.SetInitializer(new CustomInitializer());
            Create().Database.Initialize(true);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var user = modelBuilder.Entity<ApplicationUser>();

            user.Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(256)
               .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true, Order = 1 }));

        }

        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<ApplicationUserPermission> ApplicationUserPermissions { get; set; }
        public DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; set; }

        public class CustomInitializer : IDatabaseInitializer<ApplicationDbContext>
        {
            public void InitializeDatabase(ApplicationDbContext context)
            {
                
                // Seed First Super User
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                var roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

                if (!userManager.Users.Any(u => u.UserName == "superadmin@mumbile.com") && roleManager.Roles.Any(r => r.Name == "SuperUser") && context.ApplicationPermissions.Any(r => r.Name == "SuperUser"))
                {
                    var user = new ApplicationUser
                    {
                        UserName = "superadmin@mumbile.com",
                        Email = "superadmin@mumbile.com",
                        EmailConfirmed = true,
                    };

                    // Add TEMP Role
                    var roleId = roleManager.Roles.FirstOrDefault(r => r.Name == "SuperUser").Id;
                    user.Roles.Add(new IdentityUserRole { UserId = user.Id, RoleId = roleId });

                    userManager.Create(user, "Inland12!");

                    var permissionId = context.ApplicationPermissions.FirstOrDefault(r => r.Name == "SuperUser").Id;
                    context.ApplicationUserPermissions.Add(new ApplicationUserPermission { UserId = user.Id, PermissionId = permissionId });
                    context.SaveChanges();
                }
                

            }
        }
    }
}