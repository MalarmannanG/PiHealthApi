using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PatientProfileMap : EntityTypeConfiguration<PatientProfile>
    {
        public override void Map(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("PatientProfile");

            builder.HasOne(t => t.Patient)
             .WithMany(t => t.PatientProfile)
             .HasForeignKey(d => d.PatientId);

            builder.HasOne(t => t.TemplateMaster)
           .WithMany(t => t.PatientProfile)
           .HasForeignKey(d => d.TemplateMasterId);
        }
    }
}
