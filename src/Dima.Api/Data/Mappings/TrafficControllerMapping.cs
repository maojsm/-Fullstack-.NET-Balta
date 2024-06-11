using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Models;

namespace Dima.Api.Data.Mappings;

public class TrafficControllerMapping : IEntityTypeConfiguration<TrafficController>
{
    public void Configure(EntityTypeBuilder<TrafficController> builder)
    {
        builder.ToTable("TrafficControllers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CodeLocal)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(10);

        builder.HasIndex(x => x.CodeLocal)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasMaxLength(30);

        builder.Property(x => x.Description)
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.TenantId)
            .IsRequired();

        builder.HasMany(x => x.TcHardwareInRealtimes)
            .WithOne(x => x.TrafficController)
            .HasForeignKey(x => x.TrafficControllerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
