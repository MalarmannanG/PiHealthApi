using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace PiHealth.DataModel.Entity.Mapping
{
    public class AccessFunctionMap : EntityTypeConfiguration<AccessFunction>
    {
        public override void Map(EntityTypeBuilder<AccessFunction> builder)
        {
            builder.HasKey(t => t.Id);
            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
             .IsRequired();

            builder.Property(t => t.ModuleID)
             .IsRequired();

            builder.Property(t => t.FuctionCode)
             .IsRequired();

            builder.ToTable("AccessFunction");
            builder.Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar(100)");
            builder.Property(t => t.ModuleID).HasColumnName("ModuleID");
            builder.Property(t => t.FuctionCode).HasColumnName("FuctionCode").HasColumnType("nvarchar(10)");
            builder.Property(t => t.Description).HasColumnName("Description").HasColumnType("nvarchar(500)");

            builder.HasOne(t => t.AccessModule)
             .WithMany(t => t.AccessFunctions)
             .HasForeignKey(d => d.ModuleID);
        }
    }
}
