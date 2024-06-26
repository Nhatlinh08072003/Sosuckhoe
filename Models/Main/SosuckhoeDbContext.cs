using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SoSucKhoe.Models.Main;

public partial class SosuckhoeDbContext : DbContext
{
    public SosuckhoeDbContext()
    {
    }

    public SosuckhoeDbContext(DbContextOptions<SosuckhoeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lichtiem> Lichtiems { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PhieuDinhKy> PhieuDinhKies { get; set; }

    public virtual DbSet<PhieuSucKhoe> PhieuSucKhoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=UINLAN\\SQLEXPRESS;Database=SOSUCKHOE;MultipleActiveResultSets=true;User ID=admin;Password=asdasd;Trusted_Connection=True;TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lichtiem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Lichtiem__3214EC07379B22C4");

            entity.ToTable("Lichtiem");

            entity.Property(e => e.DiadiemTiem).HasMaxLength(255);
            entity.Property(e => e.GhiChu).HasMaxLength(255);
            entity.Property(e => e.LoaiVacxin).HasMaxLength(255);
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.NgayTiem).HasColumnType("date");
            entity.Property(e => e.TinhTrang).HasMaxLength(255);

            entity.HasOne(d => d.Person).WithMany(p => p.Lichtiems)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__Lichtiem__Person__403A8C7D");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC07B4D27906");

            entity.ToTable("Person");

            entity.Property(e => e.AddressUser).HasMaxLength(255);
            entity.Property(e => e.Hoten).HasMaxLength(30);
            entity.Property(e => e.Pass).HasMaxLength(255);
            entity.Property(e => e.RoleUser).HasMaxLength(30);
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PhieuDinhKy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuDin__3214EC07A8857B00");

            entity.ToTable("PhieuDinhKy");

            entity.Property(e => e.NgayKham)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Person).WithMany(p => p.PhieuDinhKies)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__PhieuDinh__Perso__3A81B327");
        });

        modelBuilder.Entity<PhieuSucKhoe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuSuc__3214EC07964C3D6D");

            entity.ToTable("PhieuSucKhoe");

            entity.Property(e => e.Diadiem).HasMaxLength(255);
            entity.Property(e => e.DinhBenh).HasMaxLength(255);
            entity.Property(e => e.HinhToaThuoc).HasMaxLength(255);
            entity.Property(e => e.Kham).HasMaxLength(255);
            entity.Property(e => e.LoaiBenh).HasMaxLength(255);
            entity.Property(e => e.LuuY).HasMaxLength(255);
            entity.Property(e => e.MoTaToaThuoc).HasMaxLength(255);
            entity.Property(e => e.NgayKham).HasColumnType("date");
            entity.Property(e => e.TenBenh).HasMaxLength(255);
            entity.Property(e => e.TrieuChung).HasMaxLength(255);

            entity.HasOne(d => d.Person).WithMany(p => p.PhieuSucKhoes)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK__PhieuSucK__Perso__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
