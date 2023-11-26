using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using Domain;
using Domain.Identity;
using Infrastructure;
using Domain.BaseData;
using Domain.Config;
using Unit = Domain.Config.Unit;

namespace Core.Db
{
    public class PanakDbContext : DbContext
    {

        public PanakDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<LoginToken> LoginTokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        #region ThisProject
        public DbSet<Organization> Organizations { set; get; }
        public DbSet<OrganizationAdmin> OrganizationAdmins { set; get; }
        public DbSet<Folder> Folders { set; get; }
        public DbSet<SimulationConfig> SimulationConfigs { set; get; }
        public DbSet<AccountConfig> AccountConfigs { set; get; }

        public DbSet<AccountUnit> AccountUnits { set; get; }
        public DbSet<Unit> Units { set; get; }
        public DbSet<Simulation> Simulations { set; get; }
        public DbSet<SimulationUser> SimulationUsers { set; get; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseLazyLoadingProxies();

        // => optionsBuilder.UseSqlServer( "Data Source=.;Initial Catalog=sql_hafari;User ID=sa;Password=1234;" ,
        //     x => x.MigrationsHistoryTable("__EFMigrationsHistory", "db_owner"));
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("db_owner");

            modelBuilder.Entity<LoginToken>()
                .HasOne(x => x.RefreshToken)
                .WithOne(a => a.LoginToken)
                .HasForeignKey<RefreshToken>(x => x.LoginTokenId);

            modelBuilder.Entity<Simulation>().HasOne(p => p.Config).WithMany(b => b.Simulations)
                .HasForeignKey(p => p.ConfigId)
                .OnDelete(DeleteBehavior.Cascade);

           


            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.ApplySoftDeleteFilter(type.ClrType);
            }

            modelBuilder.Entity<Role>().HasData(
                new Role { Name = Consts.AdminRole },
                new Role { Name = Consts.ModeratorRole },
                new Role { Name = Consts.Developer }
            );
            base.OnModelCreating(modelBuilder);
        }

    }

    public static class EFFilterExtensions
    {
        public static void ApplySoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EFFilterExtensions)
                   .GetMethods(BindingFlags.Public | BindingFlags.Static)
                   .Single(t => t.IsGenericMethod && t.Name == "ApplySoftDeleteFilter");

        public static void ApplySoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder) where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.Deleted);
        }
    }

}
