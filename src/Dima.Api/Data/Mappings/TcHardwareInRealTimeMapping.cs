using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class TcHardwareInRealTimeMapping : IEntityTypeConfiguration<TcHardwareInRealtime>
{
    public void Configure(EntityTypeBuilder<TcHardwareInRealtime> builder)
    {
        builder.ToTable("TcHardwareInRealtimes");

        builder.HasKey(x => x.Id);
    }
}
