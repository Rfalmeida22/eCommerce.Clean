using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Repositories
{
    public class Loja : ILoja
    {
        private readonly DbContext _dbContext;

        public Loja(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Lojas entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByNomeAsync(string nome, int varejistaId, int? ignorarId = null)
        {
            return await _dbContext.Set<Lojas>().AnyAsync(l => l.NmLoja == nome && l.IdVarejista == varejistaId && (!ignorarId.HasValue || l.IdLoja != ignorarId.Value));
        }

        public Task<IEnumerable<Lojas>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Lojas> GetByCnpjAsync(string cnpj)
        {
            return await _dbContext.Set<Lojas>().FirstOrDefaultAsync(x => x.CdCnpj == cnpj);
        }

        public async Task<Lojas> GetByCodigoAsync(string codigo)
        {
            return await _dbContext.Set<Lojas>().FirstOrDefaultAsync(l => l.CdLoja == codigo);
        }

        public Task<Lojas> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Lojas>> GetByLojistaIdAsync(int lojistaId)
        {
            return await _dbContext.Set<Lojas>().Where(l => l.IdLojista == lojistaId).ToListAsync();
        }

        public async Task<IEnumerable<Lojas>> GetByVarejistaIdAsync(int varejistaId)
        {
            return await _dbContext.Set<Lojas>().Where(l => l.IdVarejista == varejistaId).ToListAsync();
        }

        public Task UpdateAsync(Lojas entity)
        {
            throw new NotImplementedException();
        }
    }
}
