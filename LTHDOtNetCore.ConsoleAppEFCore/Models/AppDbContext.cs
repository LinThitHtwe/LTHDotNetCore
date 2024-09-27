using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LTHDOtNetCore.ConsoleAppEFCore.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Backpack> Backpacks { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<PieChart> PieCharts { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<PizzaExtra> PizzaExtras { get; set; }

    public virtual DbSet<PizzaOrder> PizzaOrders { get; set; }

    public virtual DbSet<PizzaOrderDetail> PizzaOrderDetails { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Weapon> Weapons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=DotnetTrainingBatch4;User Id=sa;Password=root;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Backpack>(entity =>
        {
            entity.HasIndex(e => e.CharacterId, "IX_Backpacks_CharacterId").IsUnique();

            entity.HasOne(d => d.Character).WithOne(p => p.Backpack).HasForeignKey<Backpack>(d => d.CharacterId);
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("blog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("author");
            entity.Property(e => e.BlogContent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("blogContent");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasMany(d => d.Teams).WithMany(p => p.Characters)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterTeam",
                    r => r.HasOne<Team>().WithMany().HasForeignKey("TeamsId"),
                    l => l.HasOne<Character>().WithMany().HasForeignKey("CharacterId"),
                    j =>
                    {
                        j.HasKey("CharacterId", "TeamsId");
                        j.ToTable("CharacterTeam");
                        j.HasIndex(new[] { "TeamsId" }, "IX_CharacterTeam_TeamsId");
                    });
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasMany(d => d.Students).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CourseStudent",
                    r => r.HasOne<Student>().WithMany().HasForeignKey("StudentsId"),
                    l => l.HasOne<Course>().WithMany().HasForeignKey("CoursesId"),
                    j =>
                    {
                        j.HasKey("CoursesId", "StudentsId");
                        j.ToTable("CourseStudent");
                        j.HasIndex(new[] { "StudentsId" }, "IX_CourseStudent_StudentsId");
                    });
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Employee");

            entity.Property(e => e.HourlyRate).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HoursWork).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PieChart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PieChart");

            entity.Property(e => e.PieChartId).ValueGeneratedOnAdd();
            entity.Property(e => e.PieChartName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PieChartValue).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Pizza");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<PizzaExtra>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<PizzaOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PizzaOrder");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("invoiceNo");
            entity.Property(e => e.PizzaId).HasColumnName("pizzaId");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("totalAmount");
        });

        modelBuilder.Entity<PizzaOrderDetail>(entity =>
        {
            entity.ToTable("PizzaOrderDetail");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderInvoiceNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("orderInvoiceNo");
            entity.Property(e => e.PizzaExtraId).HasColumnName("pizzaExtraId");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasIndex(e => e.AccountId, "IX_Students_AccountId");

            entity.Property(e => e.Dob).HasColumnName("DOB");

            entity.HasOne(d => d.Account).WithMany(p => p.Students).HasForeignKey(d => d.AccountId);
        });

        modelBuilder.Entity<Weapon>(entity =>
        {
            entity.HasIndex(e => e.CharacterId, "IX_Weapons_CharacterId");

            entity.HasOne(d => d.Character).WithMany(p => p.Weapons).HasForeignKey(d => d.CharacterId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
