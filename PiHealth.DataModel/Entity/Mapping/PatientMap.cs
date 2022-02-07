using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PatientMap : EntityTypeConfiguration<Patient>
    {
        public override void Map(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.PatientName)
             .IsRequired();

            builder.Property(t => t.DoctorMasterId)
             .IsRequired(false);

            builder.ToTable("Patient");

            builder.HasOne(t => t.DoctorMaster)
             .WithMany(t => t.Patients)
             .HasForeignKey(d => d.DoctorMasterId);
        }
    }
}
