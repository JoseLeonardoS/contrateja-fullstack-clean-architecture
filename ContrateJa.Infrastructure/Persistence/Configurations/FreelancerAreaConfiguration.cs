using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public sealed class FreelancerAreaConfiguration : IEntityTypeConfiguration<FreelancerArea>
{
    public void Configure(EntityTypeBuilder<FreelancerArea> builder)
    {
        builder.ToTable("freelancer_areas");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.FreelancerId)
            .IsRequired()
            .HasColumnName("freelancer_id");

        builder.OwnsOne(x => x.Area, area =>
        {
            area.OwnsOne(a => a.State, state =>
            {
                state.Property(s => s.Code)
                    .IsRequired()
                    .HasMaxLength(2)
                    .HasColumnName("state");
            });

            area.OwnsOne(a => a.City, city =>
            {
                city.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("city");
            });
        });
    }
}