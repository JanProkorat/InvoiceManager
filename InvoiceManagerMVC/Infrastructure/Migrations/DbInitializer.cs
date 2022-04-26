using System.Linq;
using System.Threading.Tasks;
using InvoiceManagerMVC.EFModels;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagerMVC.Infrastructure.Migrations;

public class DbInitializer : IDbInitializer
{
    private readonly InvoiceDbContext _context;

    public DbInitializer(InvoiceDbContext context)
    {
        _context = context;
    }

    public async Task InitAsync()
    {
        await MigrateDatabasesAsync();
    }

    private async Task MigrateDatabasesAsync()
    {
        var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await _context.Database.MigrateAsync();
        }
        
    }
}