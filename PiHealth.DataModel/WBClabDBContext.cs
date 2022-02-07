using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using PiHealth.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiHealth.DataModel
{
    public class PiHealthDBContext : DbContext
    {
        #region Ctor
        public PiHealthDBContext(DbContextOptions<PiHealthDBContext> options)
            : base(options)
        {

        }      
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            var typeFinder = new WebAppTypeFinder();

            foreach (var eachAssebly in typeFinder.GetAssemblies())
            {
                var typesToRegister = eachAssebly.GetTypes()
                .Where(type => !String.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                    type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
                foreach (var type in typesToRegister)
                {
                    dynamic configurationInstance = Activator.CreateInstance(type);
                    ModelBuilderExtensions.AddConfiguration(modelBuilder, configurationInstance);
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }  

        public Database DatabaseContext
        {
            get { return this.DatabaseContext; }
        }

       

    }
}
