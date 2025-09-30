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

    public virtual DbSet<AuditTree> AuditTrees { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<FileDetail> FileDetails { get; set; }

    public virtual DbSet<Institution> Institutions { get; set; }

    public virtual DbSet<RoleMaster> RoleMasters { get; set; }

    public virtual DbSet<SENSOR> SENSORs { get; set; }

    public virtual DbSet<SENSORTYPE> SENSORTYPEs { get; set; }

    public virtual DbSet<ScriptLog> ScriptLogs { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Tree> Trees { get; set; }

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

        modelBuilder.Entity<AuditTree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_AuditTree_Id");

            entity.ToTable("AuditTree");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Acceptable).HasDefaultValue(true);
            entity.Property(e => e.AccessionNumber).HasMaxLength(255);
            entity.Property(e => e.AddedBy).HasMaxLength(255);
            entity.Property(e => e.AuditDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.AuditId).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Deletable).HasDefaultValue(true);
            entity.Property(e => e.Disease).HasMaxLength(255);
            entity.Property(e => e.Editable).HasDefaultValue(true);
            entity.Property(e => e.Girth).HasMaxLength(255);
            entity.Property(e => e.Height).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdate).HasColumnType("datetime");
            entity.Property(e => e.Level).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Pest).HasMaxLength(255);
            entity.Property(e => e.PhysicalDamage).HasMaxLength(255);
            entity.Property(e => e.Remarks).HasMaxLength(255);
            entity.Property(e => e.ReviewedBy).HasMaxLength(255);
            entity.Property(e => e.ReviewedOn).HasColumnType("datetime");
            entity.Property(e => e.State).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.V).HasMaxLength(255);

            entity.HasOne(d => d.Tree).WithMany(p => p.AuditTrees)
                .HasForeignKey(d => d.TreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuditTree_Id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasIndex(e => e.CountryCode, "UQ__Countrie__5D9B0D2C16F29CB1").IsUnique();

            entity.HasIndex(e => e.CountryName, "UQ__Countrie__E056F20108B0A5BC").IsUnique();

            entity.Property(e => e.CountryCode).HasMaxLength(10);
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<FileDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FileDetail_Id");

            entity.ToTable("FileDetail");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Filename).HasMaxLength(255);
            entity.Property(e => e.Filetype).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Tree).WithMany(p => p.FileDetails)
                .HasForeignKey(d => d.TreeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tree_Id");
        });

        modelBuilder.Entity<Institution>(entity =>
        {
            entity.Property(e => e.InstitutionID).ValueGeneratedNever();
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

            entity.Property(e => e.RoleID).ValueGeneratedNever();
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

        modelBuilder.Entity<SENSOR>(entity =>
        {
            entity.ToTable("SENSORS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Accession_Number).HasMaxLength(255);
            entity.Property(e => e.AddedBy).HasMaxLength(50);
            entity.Property(e => e.AssetID).HasMaxLength(255);
            entity.Property(e => e.CommonName).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustID).HasMaxLength(50);
            entity.Property(e => e.Expiry_date).HasColumnType("datetime");
            entity.Property(e => e.Installation_date).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Sensitivity).HasMaxLength(255);
            entity.Property(e => e.SensorID).HasMaxLength(255);
            entity.Property(e => e.SensorUID).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.batteryPercentage).HasMaxLength(255);
            entity.Property(e => e.isHooterOn).HasDefaultValue(true);
            entity.Property(e => e.isSensitivity).HasDefaultValue(true);
            entity.Property(e => e.messageType).HasMaxLength(255);
            entity.Property(e => e.sensitivityValue).HasMaxLength(255);

            entity.HasOne(d => d.SENSORTYPE).WithMany(p => p.SENSORs)
                .HasForeignKey(d => d.SENSORTYPEID)
                .HasConstraintName("FK_SENSORS_SENSORTYPE");
        });

        modelBuilder.Entity<SENSORTYPE>(entity =>
        {
            entity.ToTable("SENSORTYPE");

            entity.HasIndex(e => e.SENSORTYPE1, "UQ__SENSORTY__B839916C8D1926B1").IsUnique();

            entity.Property(e => e.SENSORTYPEID).ValueGeneratedNever();
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SENSORTYPE1)
                .HasMaxLength(255)
                .HasColumnName("SENSORTYPE");
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

        modelBuilder.Entity<Tree>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Id");

            entity.ToTable("Tree");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccessionNumber).HasMaxLength(255);
            entity.Property(e => e.AccessionOrigin).HasMaxLength(255);
            entity.Property(e => e.AddedBy).HasMaxLength(255);
            entity.Property(e => e.AddedByName).HasMaxLength(255);
            entity.Property(e => e.Age).HasMaxLength(255);
            entity.Property(e => e.AgeUnits).HasMaxLength(255);
            entity.Property(e => e.Alerts).HasMaxLength(255);
            entity.Property(e => e.AssetId).HasMaxLength(255);
            entity.Property(e => e.AssetSubType).HasMaxLength(255);
            entity.Property(e => e.AssetType).HasMaxLength(255);
            entity.Property(e => e.BotanicalName).HasMaxLength(255);
            entity.Property(e => e.CommonName).HasMaxLength(255);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CultiverName).HasMaxLength(255);
            entity.Property(e => e.DisplayId).HasMaxLength(255);
            entity.Property(e => e.DonorOrganization).HasMaxLength(255);
            entity.Property(e => e.ExpiryDate).HasMaxLength(255);
            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Importance).HasMaxLength(255);
            entity.Property(e => e.InstallationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastAuditTime).HasMaxLength(255);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Latitude).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasMaxLength(255);
            entity.Property(e => e.OperatorFirstName).HasMaxLength(255);
            entity.Property(e => e.OperatorId).HasMaxLength(255);
            entity.Property(e => e.OperatorLastName).HasMaxLength(255);
            entity.Property(e => e.OperatorName).HasMaxLength(255);
            entity.Property(e => e.OperatorPhone).HasMaxLength(255);
            entity.Property(e => e.OperatorState).HasMaxLength(255);
            entity.Property(e => e.Origin).HasMaxLength(255);
            entity.Property(e => e.PlaceOfOrigin).HasMaxLength(255);
            entity.Property(e => e.PlantationYear).HasMaxLength(255);
            entity.Property(e => e.RfidTagCreatedOn).HasMaxLength(255);
            entity.Property(e => e.ScientificName).HasMaxLength(255);
            entity.Property(e => e.SensorType).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(255);
            entity.Property(e => e.UniqueImportance).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Value).HasMaxLength(255);

            entity.HasOne(d => d.SENSOR).WithMany(p => p.Trees)
                .HasForeignKey(d => d.SENSORID)
                .HasConstraintName("FK_TREES_SENSORID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Username, "UQ__Users__536C85E45B58185A").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534DEFBBB53").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
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
            entity.Property(e => e.Latitude).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasMaxLength(255);
            entity.Property(e => e.MfaEnabled).HasDefaultValue(false);
            entity.Property(e => e.MfaSecret).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ProfilePictureUrl).HasMaxLength(255);
            entity.Property(e => e.ResetToken).HasMaxLength(255);
            entity.Property(e => e.ResetTokenExpiry).HasColumnType("datetime");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.AddressNavigation).WithMany(p => p.Users)
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
