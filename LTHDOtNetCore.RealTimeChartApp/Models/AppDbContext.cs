using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.RealTimeChartApp.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PieChart> PieCharts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PieChart>(entity =>
        {
            // Define PieChartId as the primary key
            entity
                .HasKey(e => e.PieChartId)
                .HasName("PK_PieChart");

            // Map the table name
            entity.ToTable("PieChart");

            // Specify properties and their configurations
            entity.Property(e => e.PieChartId)
                .ValueGeneratedOnAdd(); // Auto-incremented primary key

            entity.Property(e => e.PieChartName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.PieChartValue)
                .HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
