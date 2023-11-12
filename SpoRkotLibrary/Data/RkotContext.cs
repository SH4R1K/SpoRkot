using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SpoRkotLibrary.Models;

namespace SpoRkotLibrary.Data;

public partial class RkotContext : DbContext
{
    public RkotContext()
    {
    }

    public RkotContext(DbContextOptions<RkotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HttpQuality> HttpQualities { get; set; }

    public virtual DbSet<Operator> Operators { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportInfo> ReportInfos { get; set; }

    public virtual DbSet<SmsQuality> SmsQualities { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    public virtual DbSet<VoiceQuality> VoiceQualities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=SPO_RKOT.mssql.somee.com; user id=Dimaso88_SQLLogin_1;pwd=h6ankljaeg;Trust Server Certificate=True;initial catalog=SPO_RKOT");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HttpQuality>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_QualityHTTP");

            entity.ToTable("HttpQuality");

            entity.Property(e => e.ReportId).ValueGeneratedNever();
            entity.Property(e => e.DLMeanUserDataRate).HasColumnType("decimal(6, 1)");
            entity.Property(e => e.SessionFailureRatio).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.SessionTime).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.ULMeanUserDataRate).HasColumnType("decimal(5, 1)");

            entity.HasOne(d => d.Report).WithOne(p => p.HttpQuality)
                .HasForeignKey<HttpQuality>(d => d.ReportId)
                .HasConstraintName("FK_QualityHTTP_Report");
        });

        modelBuilder.Entity<Operator>(entity =>
        {
            entity.ToTable("Operator");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_Report_1");

            entity.ToTable("Report");

            entity.HasOne(d => d.Operator).WithMany(p => p.Reports)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Report_Operator");

            entity.HasOne(d => d.ReportInfo).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReportInfoId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_Report_ReportInfo");
        });

        modelBuilder.Entity<ReportInfo>(entity =>
        {
            entity.ToTable("ReportInfo");

            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.FederalDistrict).HasMaxLength(10);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.StartDate).HasColumnType("date");
        });

        modelBuilder.Entity<SmsQuality>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_QualitySMS");

            entity.ToTable("SmsQuality");

            entity.Property(e => e.ReportId).ValueGeneratedNever();
            entity.Property(e => e.AverageDeliveryTime).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.ShareUndelivered).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Report).WithOne(p => p.SmsQuality)
                .HasForeignKey<SmsQuality>(d => d.ReportId)
                .HasConstraintName("FK_QualitySMS_Report");
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_Stats");

            entity.ToTable("Stat");

            entity.Property(e => e.ReportId).ValueGeneratedNever();

            entity.HasOne(d => d.Report).WithOne(p => p.Stat)
                .HasForeignKey<Stat>(d => d.ReportId)
                .HasConstraintName("FK_Stats_Report");
        });

        modelBuilder.Entity<VoiceQuality>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK_QualityVoice");

            entity.ToTable("VoiceQuality");

            entity.Property(e => e.ReportId).ValueGeneratedNever();
            entity.Property(e => e.CutoffRatio).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.MOSPOLQA).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.NegativeMOSSamplesRatio).HasColumnType("decimal(2, 1)");
            entity.Property(e => e.NonAccessibilityRatio).HasColumnType("decimal(2, 1)");

            entity.HasOne(d => d.Report).WithOne(p => p.VoiceQuality)
                .HasForeignKey<VoiceQuality>(d => d.ReportId)
                .HasConstraintName("FK_VoiceQuality_Report");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
