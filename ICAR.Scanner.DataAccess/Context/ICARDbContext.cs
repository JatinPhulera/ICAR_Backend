using System;
using System.Collections.Generic;
using ICAR.Scanner.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ICAR.Scanner.DataAccess.Context;

public partial class ICARDbContext : DbContext
{
    public ICARDbContext(DbContextOptions<ICARDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<ScriptLog> ScriptLogs { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.AddressId).ValueGeneratedNever();
            entity.Property(e => e.AddressLine1).HasMaxLength(255);
            entity.Property(e => e.AddressLine2).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PostalCode).HasMaxLength(20);

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_Countries");

            entity.HasOne(d => d.State).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_States");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.CountryCode, "UQ__Countrie__5D9B0D2C16F29CB1").IsUnique();

            entity.HasIndex(e => e.CountryName, "UQ__Countrie__E056F20108B0A5BC").IsUnique();

            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InstitutionAdress)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionHead)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InstitutionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.HasKey(e => e.RoleID);

            entity.ToTable("RoleMaster");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ScriptLog>(entity =>
        {
            entity.HasKey(e => e.ScriptLogId).HasName("PK__ScriptLo__F1F5130E00344A9D");

            entity.ToTable("ScriptLog");

            entity.Property(e => e.ScriptLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(255);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StateCode).HasMaxLength(10);
            entity.Property(e => e.StateName).HasMaxLength(100);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_States_Countries");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4B8D639C7").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D105341A6DC4E0").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsEmailVerified).HasDefaultValue(false);
            entity.Property(e => e.IsLocked).HasDefaultValue(false);
            entity.Property(e => e.LastLoginAt).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MfaEnabled).HasDefaultValue(false);
            entity.Property(e => e.MfaSecret).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(255);
            entity.Property(e => e.ResetToken).HasMaxLength(255);
            entity.Property(e => e.ResetTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Address).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Users_Addresses");

            entity.HasOne(d => d.Institution).WithMany(p => p.Users)
                .HasForeignKey(d => d.InstitutionID)
                .HasConstraintName("FK_Users_Institutions");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleID)
                .HasConstraintName("FK_Users_RoleMaster");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
