using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class TestMasterMap : EntityTypeConfiguration<TestMaster>
    {
        public override void Map(EntityTypeBuilder<TestMaster> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("TestMaster");

        }
    }
}
