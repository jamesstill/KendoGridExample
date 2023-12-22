using Microsoft.EntityFrameworkCore;
using WidgetApi.Interceptors;

namespace WidgetApi.Models
{

    public class WidgetContext : DbContext
    {
        // stateless singleton for all DbContext instances
        private static readonly AuditLogCommandInterceptor _interceptor = new();

        public WidgetContext(DbContextOptions<WidgetContext> options) : base(options) { }

        public DbSet<Widget> Widgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new WidgetMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_interceptor);
            base.OnConfiguring(optionsBuilder);
        }
    }
}

