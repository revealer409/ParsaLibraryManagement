using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ParsaLibraryManagement.Domain.Entities;

namespace ParsaLibraryManagement.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Configuration for the entity type <see cref="Role"/> in the database context.
    /// </summary>
    /// <remarks>
    /// This class defines how the Role entity should be mapped to the database, including its properties and relationships.
    /// </remarks>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(50);

        }
    }
}
