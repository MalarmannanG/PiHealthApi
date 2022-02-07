using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class TemplateMasterMap : EntityTypeConfiguration<TemplateMaster>
    {
        public override void Map(EntityTypeBuilder<TemplateMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("TemplateMaster");

        }
    }
}
