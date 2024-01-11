using DapperTest.Data;
using DapperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperTest.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAll();
        Task<Company> GetById(int? id);
        Task<Company> Add(Company entity);
        Task<Company> Update(Company entity);
        Task Remove(int id);

    }

    public class CompanyRepository : ICompanyRepository
    {
        public readonly DapperDbContext _dbContext;
        public CompanyRepository(DapperDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Company> Add(Company entity)
        {
            _dbContext.Companies.Add(entity);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(entity);
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _dbContext.Companies.ToListAsync();
        }

        public async Task<Company?> GetById(int? id)
        {
           return await _dbContext.Companies.FirstOrDefaultAsync(_ => _.CompanyId == id);
        }

        public async Task Remove(int id)
        {
            var company = await _dbContext.FindAsync<Company>(id);
            if (company != null)
            {
                _dbContext.Remove(company);
                await _dbContext.SaveChangesAsync();
            }
            return;
        }

        public async Task<Company> Update(Company entity)
        {
            _dbContext.Companies.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
