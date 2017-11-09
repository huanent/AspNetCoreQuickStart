using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Name).IsRequired();

            builder.Property(p => p.NickName).IsRequired();

            builder.HasMany(h => h.Permissions).WithOne().HasForeignKey(h => h.RoleId);
        }
    }
}
