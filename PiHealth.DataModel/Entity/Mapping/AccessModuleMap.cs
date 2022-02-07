using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class AccessModuleMap : EntityTypeConfiguration<AccessModule>
    {
        public override void Map(EntityTypeBuilder<AccessModule> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
             .IsRequired();

            builder.Property(t => t.ModuleCode)
             .IsRequired();

            builder.Property(t => t.Description)
             .HasMaxLength(500);

            builder.ToTable("AccessModule");
            builder.Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar(100)");
            builder.Property(t => t.ModuleCode).HasColumnName("ModuleCode").HasColumnType("nvarchar(10)");
            builder.Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar(500)");
        }
    }
}
