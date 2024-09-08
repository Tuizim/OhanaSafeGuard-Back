using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ohana.Domain;
using Ohana.View;

namespace Ohana.Infraestrutura
{
    public class OhanaDbContext : DbContext
    {
        #region atributos
        private IConfiguration _configuration;
        #endregion

        #region tabelas
        public DbSet<User> Users { get; set; }
        public DbSet<Filter> Filters {  get; set; }
        public DbSet<DefaultFilter> DefaultFilters { get; set; }
        public DbSet<CredentialStorage> credentialStorages { get; set; }
        
        #endregion
        
        #region Views
        public DbSet<UserFiltersView> UserFilterViews { get; set; }
        
        //Configuração da view
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserFiltersView>().HasNoKey();
        }

        #endregion

        #region metodos padrões

        public OhanaDbContext(IConfiguration configuration, DbContextOptions options) : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("Postgresql");
            optionsBuilder.UseNpgsql(connectionString);
        }
        #endregion
    }
}
