using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace PiHealth.DataModel.Entity.Mapping
{
    public class AuditLogMap : EntityTypeConfiguration<AuditLog>
    {
        public override void Map(EntityTypeBuilder<AuditLog> builder)
        {

            // Primary Key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
              .ValueGeneratedOnAdd();

            builder.Property(t => t.UserID)
                                .HasMaxLength(50);

            builder.Property(t => t.Controller)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Action)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(t => t.Time)
                .IsRequired();


            builder.Property(t => t.IP)
                .HasMaxLength(50);

            builder.Property(t => t.UserAgent)
                .HasMaxLength(200);

            builder.Property(t => t.Value1)
                .HasMaxLength(5000);

            builder.Property(t => t.Value2)
                .HasMaxLength(5000);

            // Table & Column Mappings
            builder.ToTable("AuditLog");
        }
    }
}
