using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class DiagnosisMasterMap : EntityTypeConfiguration<DiagnosisMaster>
    {
        public override void Map(EntityTypeBuilder<DiagnosisMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("DiagnosisMaster");

        }
    }
}
