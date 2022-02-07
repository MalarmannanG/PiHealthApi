using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class UserTokenMap : EntityTypeConfiguration<UserToken>
    {
        public override void Map(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).ValueGeneratedOnAdd();

            builder.ToTable("UserToken");

            builder.Property(t => t.AccessTokenExpiresDateTime).HasColumnName("IDAccessTokenExpiresDateTime");
            builder.Property(t => t.RefreshTokenIdHash).HasColumnName("RefreshTokenIdHash");
            builder.Property(t => t.RefreshTokenExpiresDateTime).HasColumnName("RefreshTokenExpiresDateTime");
            builder.Property(t => t.UserId).HasColumnName("UserId");

            builder.HasOne(t => t.AppUser)
                 .WithMany(t => t.Tokens)
                 .HasForeignKey(d => d.UserId);

        }
    }
}
