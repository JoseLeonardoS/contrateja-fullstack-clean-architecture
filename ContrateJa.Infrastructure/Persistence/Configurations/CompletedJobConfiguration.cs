using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public sealed class CompletedJobConfiguration : IEntityTypeConfiguration<CompletedJob>
{
    public void Configure(EntityTypeBuilder<CompletedJob> builder)
    {
        builder.ToTable("completed_jobs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.JobId)
            .IsRequired()
            .HasColumnName("job_id");

        builder.Property(x => x.FreelancerId)
            .IsRequired()
            .HasColumnName("freelancer_id");

        builder.Property(x => x.ContractorId)
            .IsRequired()
            .HasColumnName("contractor_id");

        builder.Property(x => x.CompletedAt)
            .IsRequired()
            .HasColumnName("completed_at");
    }
}