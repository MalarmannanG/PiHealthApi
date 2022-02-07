using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PatientDiagnosisMap : EntityTypeConfiguration<PatientDiagnosis>
    {
        public override void Map(EntityTypeBuilder<PatientDiagnosis> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("PatientDiagnosis");


            builder.HasOne(t => t.DiagnosisMaster)
             .WithMany(t => t.PatientDiagnosis)
             .HasForeignKey(d => d.DiagnosisMasterId);

            builder.HasOne(t => t.PatientProfile)
           .WithMany(t => t.PatientDiagnosis)
           .HasForeignKey(d => d.PatientProfileId);

        }
    }
}
