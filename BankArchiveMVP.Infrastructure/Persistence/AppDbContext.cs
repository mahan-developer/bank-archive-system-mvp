using BankArchiveMVP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankArchiveMVP.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentIndex> DocumentIndexes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customers
            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.CustomerNo)
                .IsUnique();

            // Cases
            modelBuilder.Entity<Case>()
                .HasIndex(x => x.CaseNo)
                .IsUnique();

            modelBuilder.Entity<Case>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Documents
            modelBuilder.Entity<Document>()
                .HasIndex(x => new { x.CaseId, x.FileHash })
                .IsUnique();

            modelBuilder.Entity<Document>()
                .HasOne<Case>()
                .WithMany()
                .HasForeignKey(x => x.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // DocumentIndex (1:1)
            modelBuilder.Entity<DocumentIndex>()
                .HasOne<Document>()
                .WithOne()
                .HasForeignKey<DocumentIndex>(x => x.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            // AuditLogs index
            modelBuilder.Entity<AuditLog>()
                .HasIndex(x => new { x.EntityType, x.EntityId, x.CreatedAt });

            base.OnModelCreating(modelBuilder);
        }

    }
}
