using AbstractSushiBarModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AbstractSushiBarService
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabaseSB_WPF")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Visitor> Visitors { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<Cook> Cooks { get; set; }

        public virtual DbSet<Zakaz> Zakazs { get; set; }

        public virtual DbSet<Sushi> Sushis { get; set; }

        public virtual DbSet<SushiIngredient> SushiIngredients { get; set; }

        public virtual DbSet<Storage> Storages { get; set; }

        public virtual DbSet<StorageIngredient> StorageIngredients { get; set; }
    }
}
