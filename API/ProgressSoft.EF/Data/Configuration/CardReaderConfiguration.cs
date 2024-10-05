using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressSoft.Core.Entites;

namespace ProgressSoft.EF.Data.Configuration
{
    public class CardReaderConfiguration : IEntityTypeConfiguration<CardReader>
    {
        public void Configure(EntityTypeBuilder<CardReader> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();

            builder.Property(x => x.Gender)
                .HasMaxLength(10).IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(250).IsRequired();

            builder.Property(x => x.Phone)
                .HasMaxLength(50).IsRequired();

            builder.Property(x => x.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(150)
                .IsRequired();
        }
    }
}
