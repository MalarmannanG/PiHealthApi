using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class TemplatePrescriptionMap : EntityTypeConfiguration<TemplatePrescription>
    {
        public override void Map(EntityTypeBuilder<TemplatePrescription> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("TemplatePrescription");

            builder.HasOne(t => t.TemplateMaster)
             .WithMany(t => t.TemplatePrescriptions)
             .HasForeignKey(d => d.TemplateMasterId);
        }
    }
}
