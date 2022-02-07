using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class AppUserMap : EntityTypeConfiguration<AppUser>
    {
        public override void Map(EntityTypeBuilder<AppUser> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Email)
                .HasMaxLength(35);

            builder.Property(t => t.Password)
                .IsRequired();

            builder.Property(t => t.PhoneNo)
                .HasMaxLength(20);


            builder.Property(t => t.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.UserType)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            builder.ToTable("AppUser");
            builder.Property(t => t.Id).HasColumnName("ID");
        
            builder.Property(t => t.Name).HasColumnName("Name");
            builder.Property(t => t.Email).HasColumnName("Email");
            builder.Property(t => t.PhoneNo).HasColumnName("PhoneNo");
            builder.Property(t => t.Username).HasColumnName("Username");
            builder.Property(t => t.Password).HasColumnName("Password");
            builder.Property(t => t.SerialNumber).HasColumnName("SerialNumber");
            builder.Property(t => t.Gender).HasColumnName("Gender");
            builder.Property(t => t.Address).HasColumnName("Address");
            builder.Property(t => t.UserType).HasColumnName("UserType");
            builder.Property(t => t.IsActive).HasColumnName("IsActive");
            builder.Property(t => t.CreatedDate).HasColumnName("CreatedDate");        
            builder.Property(t => t.LastLoggedIn).HasColumnName("LastLoggedIn");


        }
    }
}
