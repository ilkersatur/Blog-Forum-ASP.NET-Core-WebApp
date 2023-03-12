using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApp.Models.Config
{
    public class KonuCFG : IEntityTypeConfiguration<Konu>
    {
        public void Configure(EntityTypeBuilder<Konu> builder)
        {
            builder.Property(x => x.KonuAdi).IsRequired().HasColumnType("varchar").HasMaxLength(200);

        }
    }
}
