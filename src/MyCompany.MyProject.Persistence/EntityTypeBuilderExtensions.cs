using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCompany.MyProject.Domain;

namespace MyCompany.MyProject.Persistence.EntityTypeConfigurations
{
    internal static class EntityTypeBuilderExtensions
    {
        internal static void InitBuildInProperty<T>(this EntityTypeBuilder<T> builder, string datebasePrimaryKey = "Id") where T : EntityBase
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
