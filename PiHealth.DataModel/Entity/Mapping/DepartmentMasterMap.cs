using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class DepartmentMasterMap : EntityTypeConfiguration<DepartmentMaster>    
    {
        public override void Map(EntityTypeBuilder<DepartmentMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
             .IsRequired();

            builder.Property(t => t.Description)
              .IsRequired(false);

            builder.ToTable("DepartmentMaster");
        }
      }
}
