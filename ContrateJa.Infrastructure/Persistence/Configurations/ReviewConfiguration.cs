using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).ValueGeneratedOnAdd();

        builder.Property(r => r.ReviewerId)
            .IsRequired()
            .HasColumnName("reviewer_id");

        builder.Property(r => r.ReviewedId)
            .IsRequired()
            .HasColumnName("reviewed_id");

        builder.Property(r => r.JobId)
            .IsRequired()
            .HasColumnName("job_id");

        builder.Property(r => r.Rating)
            .IsRequired()
            .HasColumnName("rating");

        builder.Property(r => r.Comment)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("comment");

        builder.Property(r => r.SubmittedAt)
            .IsRequired()
            .HasColumnName("submitted_at");
    }
}