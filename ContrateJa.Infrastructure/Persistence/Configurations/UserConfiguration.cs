using ContrateJa.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContrateJa.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(user => user.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(75)
                .IsRequired();

            name.Property(n => n.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(75)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Phone, phone =>
        {
            phone.Property(p => p.CountryCode)
                .HasColumnName("country_code")
                .HasConversion<string>()
                .HasMaxLength(3)
                .IsRequired();
            
            phone.Property(p => p.NationalNumber)
                .HasColumnName("number")
                .HasMaxLength(20)
                .IsRequired();
        });

        builder.OwnsOne(user => user.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("email")
                .HasMaxLength(255)
                .IsRequired();
            
            email.HasIndex(e => e.Address).IsUnique();
        });

        builder.OwnsOne(user => user.PasswordHash, passwordHash =>
        {
            passwordHash.Property(p => p.Hash)
                .HasColumnName("password_hash")
                .HasMaxLength(60)
                .IsRequired();
        });

        builder.Property(user => user.AccountType)
            .HasColumnName("account_type")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(user => user.IsAvailable)
            .HasColumnName("is_available")
            .IsRequired();

        builder.OwnsOne(user => user.Document, document =>
        {
            document.Property(d => d.Value)
                .HasColumnName("document")
                .HasMaxLength(14)
                .IsRequired();
        });
        
        builder.OwnsOne(user => user.State, state =>
        {
            state.Property(s => s.Code)
                .HasColumnName("state_code")
                .HasMaxLength(2)
                .IsRequired();
        });

        builder.OwnsOne(user => user.City, city =>
        {
            city.Property(c => c.Name)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();
        });
        
        builder.OwnsOne(user => user.Street, street =>
        {
            street.Property(s => s.Name)
                .HasColumnName("street")
                .HasMaxLength(150)
                .IsRequired();
        });
        
        builder.OwnsOne(user => user.ZipCode, zipCode =>
        {
            zipCode.Property(z => z.Value)
                .HasColumnName("zip_code")
                .HasMaxLength(8)
                .IsRequired();
        });
    }
}