using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class AccessRoleFunctionMap : EntityTypeConfiguration<AccessRoleFunction>
    {
        public override void Map(EntityTypeBuilder<AccessRoleFunction> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Role)
             .IsRequired();

            builder.ToTable("AccessRoleFunction");
            builder.Property(t => t.Role).HasColumnName("Role").HasColumnType("nvarchar(100)");
            builder.Property(t => t.FunctionID).HasColumnName("FunctionID");

            builder.HasOne(t => t.AccessFunctions)
             .WithMany(t => t.AccessRole)
             .HasForeignKey(d => d.FunctionID);
        }
    }
}
