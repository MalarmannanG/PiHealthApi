using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class VitalsReportMap : EntityTypeConfiguration<VitalsReport>    
    {
        public override void Map(EntityTypeBuilder<VitalsReport> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("VitalsReport");

            builder.HasOne(t => t.Patient)
             .WithMany(t => t.VitalsReports)
             .HasForeignKey(d => d.PatientID);

            builder.HasOne(t => t.Appointment)
            .WithOne(t => t.VitalsReport)
            .HasForeignKey<VitalsReport>(d => d.AppointmentID);
        }
      }
}
