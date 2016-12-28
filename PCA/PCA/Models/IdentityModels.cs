using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCA.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<ContractCostPhase> ContractCostPhases { get; set; }
        public virtual DbSet<ContractExhibit> ContractExhibits { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<ContractorType> ContractorType { get; set; }
        public virtual DbSet<ContractorTypeUnion> ContractorTypeUnions { get; set; }
        public virtual DbSet<DailyReport> DailyReport { get; set; }
        public virtual DbSet<DailyReportPicture> DailyReportPicture { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Phase> Phases { get; set; }
        public virtual DbSet<Position> Positions { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<SubPhase> SubPhases { get; set; }
        public virtual DbSet<WorkItem> WorkItems { get; set; }

        public System.Data.Entity.DbSet<PCA.Models.ContractGeneralCondition> ContractGeneralConditions { get; set; }
    } 
}