using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository.Models;

namespace Repository.DBContext
{
    public partial class RolodexContext : DbContext
    {
        public RolodexContext()
        {
        }

        public RolodexContext(DbContextOptions<RolodexContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EmailAddress> EmailAddresses { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public virtual DbSet<PhysicalAddress> PhysicalAddresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__EmailAdd__A9D10534017F4842")
                    .IsUnique();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.EmailAddresses)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK__EmailAddr__Perso__2A4B4B5E");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(128);

                entity.Property(e => e.LastName).HasMaxLength(128);
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasIndex(e => e.Number, "UQ__PhoneNum__78A1A19DEA795CC3")
                    .IsUnique();

                entity.Property(e => e.Number).HasMaxLength(16);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK__PhoneNumb__Perso__37A5467C");
            });

            modelBuilder.Entity<PhysicalAddress>(entity =>
            {
                entity.Property(e => e.City).HasMaxLength(128);

                entity.Property(e => e.PostalCode).HasMaxLength(32);

                entity.Property(e => e.Region).HasMaxLength(64);

                entity.Property(e => e.StreetAddress).HasMaxLength(256);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.PhysicalAddresses)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK__PhysicalA__Perso__33D4B598");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
