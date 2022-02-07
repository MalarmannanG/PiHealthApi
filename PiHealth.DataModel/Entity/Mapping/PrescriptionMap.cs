using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PrescriptionMap : EntityTypeConfiguration<Prescription>
    {
        public override void Map(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("Prescription");

            builder.HasOne(t => t.PatientProfile)
             .WithMany(t => t.Prescriptions)
             .HasForeignKey(d => d.PatientProfileId);
        }
    }
}
