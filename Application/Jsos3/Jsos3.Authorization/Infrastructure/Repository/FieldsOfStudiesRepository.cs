using Dapper;
using Jsos3.Authorization.Models;
using Jsos3.Shared.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jsos3.Authorization.Infrastructure.Repository
{
    public interface IFieldsOfStudiesRepository
    {
        public Task<List<FieldOfStudyDto>> GetFieldsOfStudies();
    }
    internal class FieldsOfStudiesRepository(IDbConnectionFactory _dbConnectionFactory) : IFieldsOfStudiesRepository
    {
        public async Task<List<FieldOfStudyDto>> GetFieldsOfStudies()
        {
            using var _dbConnection = await _dbConnectionFactory.GetOpenAdministrationConnectionAsync();
            string query = "SELECT * FROM FieldOfStudy";
            return _dbConnection.Query<FieldOfStudyDto>(query).ToList();
        }
    }
}