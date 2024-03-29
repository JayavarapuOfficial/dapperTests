﻿using Dapper;
using DapperTest.Models;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DapperTest.Repositories
{
    //public interface IDapperRepository
    //{
    //    Task<IEnumerable<Company>> GetAll();
    //    Task<Company> GetById(int? id);
    //    Task<Company> Add(Company entity);
    //    Task<Company> Update(Company entity);
    //    Task Remove(int id);
    //}


    public class DapperRepository : ICompanyRepository
    {
        private readonly IDbConnection _db;
        public DapperRepository(IConfiguration config)
        {
            _db = new SqlConnection(config.GetConnectionString("DefaultConnectionStrings"));
            
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var sql = "SELECT * FROM COMPANIES";
            var result = _db.Query<Company>(sql).ToList(); ;
            return await Task.FromResult(result.ToArray());
        }

        public Task<Company> Add(Company entity)
        {
            var sql = "INSERT INTO [dbo].[Companies] ([Name],[Address],[City],[State],[PostalCode]) VALUES" +
                " (@Name,@Address,@City,@State,@PostalCode);SELECT CAST(SCOPE_IDENTITY() as  int);";
            var newCompany = _db.Query<Company>(sql, new
            {
                @Name = entity.Name,
                @Address = entity.Address,
                @City = entity.City,
                @State = entity.State,
                @PostalCode = entity.PostalCode
            }).Single();
            entity.CompanyId = newCompany.CompanyId;
            return Task.FromResult(entity);
        }

        public async Task<Company> GetById(int? id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            var company = _db.Query<Company>(sql, new { @CompanyId = id }).Single();;
            return await Task.FromResult(company);
        }

        public async Task Remove(int id)
        {
             var sql = "DELETE FROM Companies where CompanyId = @CompanyId";
            var company = _db.Query<Company>(sql, new { @CompanyId = id });
            await Task.FromResult(company);
        }

        public async Task<Company> Update(Company entity)
        {
            var sql = "UPDATE Companies SET Name = @Name, Address= @Address, City = @City, State= @State, PostalCode=@PostalCode  WHERE  CompanyId = @CompanyId;";

            _db.Execute(sql, entity);
;            return await Task.FromResult(entity);
        }
    }
}
