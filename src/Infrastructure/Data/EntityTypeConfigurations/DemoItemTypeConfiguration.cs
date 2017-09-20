using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntityTypeConfigurations
{
    public class DemoItemTypeConfiguration : IEntityTypeConfiguration<DemoItem>
    {
        public void Configure(EntityTypeBuilder<DemoItem> builder)
        {
            builder.HasKey(h => h.Id);
        }
    }
}
