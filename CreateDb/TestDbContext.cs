using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreateDb
{
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<A> A { get; set; }

    public DbSet<ADetail> ADetail { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.UseCollation("Chinese_Taiwan_Stroke_CI_AS");


        modelBuilder.ApplyConfiguration(new AConfiguration());
        modelBuilder.ApplyConfiguration(new ADetailConfiguration());

    }
}

internal class AConfiguration : IEntityTypeConfiguration<A>
{
    public void Configure(EntityTypeBuilder<A> builder)
    {
        builder.ToTable(nameof(A), DbParameter.DefaultSchema);

        builder.HasKey(x => new
                            {
                                x.Id,
                            })
                       .IsClustered();

        builder.HasIndex(u => u.Guid)
               .HasName($"IX_{nameof(A)}_{nameof(A.Guid)}")
               .IsUnique();

        builder.Property(x => x.Id)
               .IsRequired()
               .UseIdentityColumn(1, 1)
               .HasColumnType(SqlDbTypes.Int)
               .HasComment("ID");

        builder.Property(x => x.Guid)
               .IsRequired()
               .HasColumnType(SqlDbTypes.Uniqueidentifier)
               .HasComment("Guid");

        builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType(SqlDbTypes.Nvarchar(50))
               .HasComment("名稱");

         // 以下是 FK 設定
         builder.HasMany(a => a.Details)
                .WithOne(d => d.A);
    }
}

internal class ADetailConfiguration : IEntityTypeConfiguration<ADetail>
{
    public void Configure(EntityTypeBuilder<ADetail> builder)
    {
        builder.ToTable(nameof(ADetail), DbParameter.DefaultSchema);

        builder.HasKey(x => new
                            {
                                x.Id,
                            })
                       .IsClustered();

        builder.HasIndex(u => u.Guid)
               .HasName($"IX_{nameof(ADetail)}_{nameof(ADetail.Guid)}")
               .IsUnique();

        builder.Property(x => x.Id)
               .IsRequired()
               .UseIdentityColumn(1, 1)
               .HasColumnType(SqlDbTypes.Int)
               .HasComment("ID");

        builder.Property(x => x.Guid)
               .IsRequired()
               .HasColumnType(SqlDbTypes.Uniqueidentifier)
               .HasComment("Guid");

        builder.Property(x => x.AGuid)
               .IsRequired()
               .HasColumnType(SqlDbTypes.Uniqueidentifier)
               .HasComment("A Guid");

        builder.Property(x => x.Name)
               .IsRequired()
               .HasColumnType(SqlDbTypes.Nvarchar(50))
               .HasComment("名稱");


         // 以下是 FK 設定
         builder.HasOne(x => x.A)
                .WithMany()
                .IsRequired()
                .HasConstraintName($"FK_{nameof(ADetail)}_{nameof(ADetail.AGuid)}_{nameof(A)}_{nameof(A.Guid)}")
                .HasPrincipalKey(x => x.Guid)
                .OnDelete(DeleteBehavior.NoAction);

    }
}
}
