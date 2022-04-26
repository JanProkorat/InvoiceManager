

#nullable disable

using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InvoiceManagerMVC.EFModels
{
    public partial class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext()
        {
        }

        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
            : base(options)
        {
        }
        
        public bool HasActiveTransaction => Database.CurrentTransaction != null;

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=Data");
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
        
        // public Task<IDbContextTransaction> BeginTransactionAsync()
        // {
        //     return HasActiveTransaction ? Task.FromResult<IDbContextTransaction>(null) : Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        // }
        //
        // public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        // {
        //     if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        //     if (transaction != Database.CurrentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");
        //
        //     try
        //     {
        //         await SaveChangesAsync();
        //         await transaction.CommitAsync();
        //     }
        //     catch
        //     {
        //         await RollbackTransactionAsync();
        //         throw;
        //     }
        //     finally
        //     {
        //         if (Database.CurrentTransaction != null)
        //         {
        //             await Database.CurrentTransaction.DisposeAsync();
        //         }
        //     }
        // }
        //
        // public async Task RollbackTransactionAsync()
        // {
        //     try
        //     {
        //         if (Database.CurrentTransaction != null)
        //         {
        //             await Database.CurrentTransaction.RollbackAsync();
        //         }
        //     }
        //     finally
        //     {
        //         if (Database.CurrentTransaction != null)
        //         {
        //             await Database.CurrentTransaction.DisposeAsync();
        //         }
        //     }
        // }
    }
}
