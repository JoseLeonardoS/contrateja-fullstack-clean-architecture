using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public sealed class ProposalConfiguration : IEntityTypeConfiguration<Proposal>
{
    public void Configure(EntityTypeBuilder<Proposal> builder)
    {
        builder.ToTable("proposals");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(proposal => proposal.JobId)
            .IsRequired()
            .HasColumnName("job_id");
        
        builder.Property(proposal => proposal.FreelancerId)
            .IsRequired()
            .HasColumnName("freelancer_id");

        builder.OwnsOne(proposal => proposal.Money, amount =>
        {
            amount.Property(a => a.Amount)
                .IsRequired()
                .HasColumnName("amount");
            
            amount.Property(a => a.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("currency");
        });
        
        builder.Property(proposal => proposal.CoverLetter)
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName("cover_letter");
        
        builder.Property(proposal => proposal.Status)
            .IsRequired()
            .HasColumnName("status")
            .HasConversion<string>();
        
        builder.Property(proposal => proposal.SubmittedAt)
            .HasColumnName("submitted_at");
    }
}