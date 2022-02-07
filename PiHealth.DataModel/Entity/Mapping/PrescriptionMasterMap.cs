using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PrescriptionMasterMap : EntityTypeConfiguration<PrescriptionMaster>
    {
        public override void Map(EntityTypeBuilder<PrescriptionMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("PrescriptionMaster");
        }
    }
}
