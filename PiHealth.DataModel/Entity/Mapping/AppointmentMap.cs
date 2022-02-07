using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class AppointmentMap : EntityTypeConfiguration<Appointment>    
    {
        public override void Map(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            
            builder.Property(t => t.Description)
              .IsRequired(false);

            builder.ToTable("Appointment");
            

            builder.HasOne(t => t.AppUser)
             .WithMany(t => t.Appointments)
             .HasForeignKey(d => d.AppUserId);

            builder.HasOne(t => t.Patient)
             .WithMany(t => t.Appointments)
             .HasForeignKey(d => d.PatientId);

            builder.HasOne(t => t.PatientProfile)
            .WithOne(t => t.Appointment)
            .HasForeignKey<PatientProfile>(d => d.AppointmentId);
        }
      }
}
