using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class PatientFilesMap : EntityTypeConfiguration<PatientFiles>    
    {
        public override void Map(EntityTypeBuilder<PatientFiles> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("PatientFiles");

            builder.HasOne(t => t.Patient)
             .WithMany(t => t.PatientFiles)
             .HasForeignKey(d => d.PatientID);

            builder.HasOne(t => t.Appointment)
            .WithMany(t => t.PatientFiles)
            .HasForeignKey(d => d.AppointmentID);
        }
      }
}
