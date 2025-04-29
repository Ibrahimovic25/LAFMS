using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LAFMS.Models;

public partial class ManagementSystemContext : DbContext
{
    public ManagementSystemContext()
    {
    }

    public ManagementSystemContext(DbContextOptions<ManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Claimant> Claimants { get; set; }

    public virtual DbSet<Finder> Finders { get; set; }

    public virtual DbSet<FoundRecord> FoundRecords { get; set; }

    public virtual DbSet<Item> Items { get; set; }

   // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
  //   => optionsBuilder.UseNpgsql("Host=ep-purple-union-a4wb8u2u-pooler.us-east-1.aws.neon.tech;Database=ManagementSystem;Username=ManagementSystem_owner;Password=npg_qbrOSPzBX89l");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claimant>(entity =>
        {
            entity.HasKey(e => e.ClaimantId).HasName("claimant_pkey");

            entity.ToTable("claimant");

            entity.Property(e => e.ClaimantId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("claimant_id");
            entity.Property(e => e.ClaimantName)
                .HasMaxLength(25)
                .HasColumnName("claimant_name");
        });

        modelBuilder.Entity<Finder>(entity =>
        {
            entity.HasKey(e => e.FinderId).HasName("finder_pkey");

            entity.ToTable("finder");

            entity.Property(e => e.FinderId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("finder_id");
            entity.Property(e => e.FinderContact)
                .HasMaxLength(15)
                .HasColumnName("finder_contact");
            entity.Property(e => e.FinderName)
                .HasMaxLength(25)
                .HasColumnName("finder_name");
        });

        modelBuilder.Entity<FoundRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("found_record_pkey");

            entity.ToTable("found_record");

            entity.HasIndex(e => new { e.ItemId, e.FinderId }, "found_record_item_id_finder_id_key").IsUnique();

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.ClaimDate)
                .HasMaxLength(15)
                .HasColumnName("claim_date");
            entity.Property(e => e.ClaimantId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("claimant_id");
            entity.Property(e => e.FinderId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("finder_id");
            entity.Property(e => e.FoundDate)
                .HasMaxLength(15)
                .HasColumnName("found_date");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.HasOne(d => d.Claimant).WithMany(p => p.FoundRecords)
                .HasForeignKey(d => d.ClaimantId)
                .HasConstraintName("found_record_claimant_id_fkey");

            entity.HasOne(d => d.Finder).WithMany(p => p.FoundRecords)
                .HasForeignKey(d => d.FinderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("found_record_finder_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.FoundRecords)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("found_record_item_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.ItemId)
                .ValueGeneratedNever()
                .HasColumnName("item_id");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(25)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemName)
                .HasMaxLength(25)
                .HasColumnName("item_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
