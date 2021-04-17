using Drugstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Drugstore.Infra.Context
{
    public class DrugstoreContext : DbContext
    {
        public DrugstoreContext(DbContextOptions<DrugstoreContext> options):base(options)
        {

        }
        public DbSet<Shopkeeper> Shopkeepers { get; set;}
    }
}
