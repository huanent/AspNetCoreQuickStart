using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyCompany.MyProject.Infrastructure
{
    internal static class EntityTypeBuilderExtensions
    {
        internal static void InitBuildInProperty<T>(this EntityTypeBuilder<T> builder) where T : EntityBase
        {
            builder.HasKey("Id");

            builder.Property("CreateDate")
                .IsRequired();

            builder.Property("ModifiedDate")
                .IsRequired();
        }
    }
}
