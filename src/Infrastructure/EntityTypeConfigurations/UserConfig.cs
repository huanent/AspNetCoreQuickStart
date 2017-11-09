using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(p => p.NickName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(p => p.Pwd)
                .HasMaxLength(64)
                .IsRequired();

            builder.HasMany(m => m.Permissions).WithOne().HasForeignKey(h => h.UserId);
        }
    }
}
