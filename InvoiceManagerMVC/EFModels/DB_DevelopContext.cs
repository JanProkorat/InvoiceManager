using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace InvoiceManagerMVC.EFModels
{
    public partial class DB_DevelopContext : DbContext
    {
        public DB_DevelopContext()
        {
        }

        public DB_DevelopContext(DbContextOptions<DB_DevelopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Data");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.XId)
                    .HasName("PK__Invoice__313C590AED48FA28");

                entity.ToTable("Invoice");

                entity.Property(e => e.XId).HasColumnName("xID");

                entity.Property(e => e.InvoiceName)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("invoice_name");

                entity.Property(e => e.State).HasColumnName("state");
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.HasKey(e => e.XId)
                    .HasName("PK__InvoiceI__313C590ACEB8958D");

                entity.ToTable("InvoiceItem");

                entity.Property(e => e.XId).HasColumnName("xID");

                entity.Property(e => e.AmountToPay).HasColumnName("amount_to_pay");

                entity.Property(e => e.Deadline)
                    .HasColumnType("datetime")
                    .HasColumnName("deadline");

                entity.Property(e => e.KInvoice).HasColumnName("k_invoice");

                entity.Property(e => e.Receiver)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("receiver");

                entity.HasOne(d => d.KInvoiceNavigation)
                    .WithMany(p => p.InvoiceItems)
                    .HasForeignKey(d => d.KInvoice)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__InvoiceIt__k_inv__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
