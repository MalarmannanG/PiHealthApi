using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class DoctorMasterMap : EntityTypeConfiguration<DoctorMaster>    
    {
        public override void Map(EntityTypeBuilder<DoctorMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
             .IsRequired();

            builder.Property(t => t.Percentage)
              .IsRequired();

            builder.ToTable("DoctorMaster");
        }
      }
}
