using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InvoiceManagerMVC.EFModels;

namespace InvoiceManagerMVC.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InvoiceDbContext _context;
        private readonly IMapper _mapper;
        private Dictionary<string, object> _repositories;

        public InvoiceDbContext Context { get => _context; }
        public IMapper Mapper { get => _mapper; }

        public UnitOfWork(InvoiceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public TRepo TypedRepository<TRepo>() where TRepo : class
        {
            _repositories ??= new Dictionary<string, object>();

            var type = typeof(TRepo).Name;

            if (_repositories.ContainsKey(type)) return (TRepo) _repositories[type];
            
            var repositoryInstance = (TRepo)Activator.CreateInstance(typeof(TRepo), _context);
            _repositories.Add(type, repositoryInstance);
            return (TRepo)_repositories[type];
        }
        
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        
    }
}