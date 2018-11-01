using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyCompany.MyProject.Infrastructure.EntityTypeConfigurations
{
    public static class EntityTypeBuilderExtensions
    {
        public static void InitBuildInProperty<T>(this EntityTypeBuilder<T> builder, string datebasePrimaryKey = "Id") where T : class
        {
            builder.HasKey("Id");

            builder.Property("Id").HasColumnName(datebasePrimaryKey);

            builder.Property("CreateDate")
                .IsRequired();

            builder.Property("ModifiedDate")
                .IsRequired();
        }
    }
}
