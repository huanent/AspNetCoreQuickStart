using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(p => p.Controller)
                .IsRequired();

            builder.Property(p => p.Action)
                .IsRequired();

            builder.Property(p => p.HttpMethod)
                .IsRequired();
        }
    }
}
