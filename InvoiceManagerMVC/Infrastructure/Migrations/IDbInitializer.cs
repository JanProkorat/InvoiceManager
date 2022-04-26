using System.Threading.Tasks;

namespace InvoiceManagerMVC.Infrastructure.Migrations;

public interface IDbInitializer
{
    Task InitAsync();
}