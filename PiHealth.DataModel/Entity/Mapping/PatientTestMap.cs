using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class PatientTestMap : EntityTypeConfiguration<PatientTest>
    {
        public override void Map(EntityTypeBuilder<PatientTest> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("PatientTest");

            builder.HasOne(t => t.PatientProfile)
             .WithMany(t => t.PatientTests)
             .HasForeignKey(d => d.PatientProfileId);

            builder.HasOne(t => t.TestMaster)
           .WithMany(t => t.PatientTests)
           .HasForeignKey(d => d.TestMasterId);
        }
    }
}
