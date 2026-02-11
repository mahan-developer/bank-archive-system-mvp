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
                .HasOne(c => c.Customer)
                .WithMany(cu => cu.Cases)
                .HasForeignKey(c => c.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Documents
            modelBuilder.Entity<Document>()
                .HasIndex(d => new { d.CaseId, d.FileHash })
                .IsUnique();

            modelBuilder.Entity<Document>()
                .HasOne(d => d.Case)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // DocumentIndex (1:1) - مشخص کردن dependent با FK
            modelBuilder.Entity<Document>()
                .HasOne(d => d.DocumentIndex)
                .WithOne(i => i.Document)
                .HasForeignKey<DocumentIndex>(i => i.DocumentId)
                .OnDelete(DeleteBehavior.Cascade);

            // AuditLogs index
            modelBuilder.Entity<AuditLog>()
                .HasIndex(a => new { a.EntityType, a.EntityId, a.CreatedAt });

            base.OnModelCreating(modelBuilder);
        }


    }
}
