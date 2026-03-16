using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public sealed class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.ToTable("jobs");
        builder.HasKey(job => job.Id);
        builder.Property(job => job.Id).ValueGeneratedOnAdd();
        
        builder.Property(job => job.ContractorId)
            .IsRequired()
            .HasColumnName("contractor_id");
        
        builder.Property(job => job.Title)
            .IsRequired()
            .HasMaxLength(150)
            .HasColumnName("title");
        
        builder.Property(job => job.Description)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("description");

        builder.OwnsOne(job => job.State, state =>
        {
            state.Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(2)
                .HasColumnName("state");
        });

        builder.OwnsOne(job => job.City, city =>
        {
            city.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("city");
        });

        builder.OwnsOne(job => job.Street, street =>
        {
            street.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("street");
        });

        builder.OwnsOne(job => job.ZipCode, zipCode =>
        {
            zipCode.Property(z => z.Value)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("zip_code");
        });
        
        builder.Property(job => job.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasColumnName("status");
    }
}