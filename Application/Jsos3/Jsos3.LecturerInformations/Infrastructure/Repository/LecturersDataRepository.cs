using Dapper;
using Jsos3.LecturerInformations.Infrastructure.Models;
using Jsos3.Shared.Db;


namespace Jsos3.LecturerInformations.Infrastructure.Repository;

internal interface ILecturersDataRepository
{
    Task<List<Lecturer>> GetPagedLecturers(string? searchTerm, int offset, int rowsCount);
    Task<Dictionary<int, List<string>>> GetLecturersPhones(IEnumerable<int> lecturerIds);
    Task<Dictionary<int, List<string>>> GetLecturersEmails(IEnumerable<int> lecturerIds);
    Task<int> GetLecturersPagesCount(string? searchTerm);
}

internal class LecturersDataRepository : ILecturersDataRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public LecturersDataRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<List<Lecturer>> GetPagedLecturers(string? searchTerm, int offset, int rowsCount)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var search = GetSearchTerm(searchTerm);
        var query = @$"
SELECT
    l.Id AS [{nameof(Lecturer.Id)}],
    u.Name AS [{nameof(Lecturer.Name)}], 
    u.Surname AS [{nameof(Lecturer.Surname)}]
FROM [Lecturer] l
INNER JOIN [User] u ON l.UserId = u.Id
WHERE @search IS NULL
        OR u.Name LIKE @search
        OR u.Surname LIKE @search
        OR CONCAT(u.Name, ' ', u.Surname) LIKE @search
ORDER BY Surname
OFFSET @offset ROWS FETCH NEXT @rowsCount ROWS ONLY;
";
        var queryResult = await connection.QueryAsync<Lecturer>(
            query,
            new
            { 
                search,
                offset,
                rowsCount
            });

        return queryResult.ToList();
    }

    public async Task<Dictionary<int, List<string>>> GetLecturersEmails(IEnumerable<int> lecturerIds)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    l.Id AS [{nameof(LecturerEmail.Id)}],
    e.Email AS [{nameof(LecturerEmail.Email)}]
FROM [Lecturer] l
INNER JOIN [User] u ON l.UserId = u.Id
INNER JOIN [Email] e ON e.UserId = u.Id
WHERE l.Id IN @lecturerIds;
";
        var queryResult = await connection.QueryAsync<LecturerEmail>(query, new { lecturerIds });

        return queryResult
            .GroupBy(x => x.Id)
            .ToDictionary(x => x.Key, x => x.Select(e => e.Email).ToList());
    }

    public async Task<Dictionary<int, List<string>>> GetLecturersPhones(IEnumerable<int> lecturerIds)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var query = @$"
SELECT
    l.Id AS [{nameof(LecturerPhone.Id)}],
    wp.Phone AS [{nameof(LecturerPhone.Phone)}]
FROM [Lecturer] l
INNER JOIN [User] u ON l.UserId = u.Id
INNER JOIN [WorkPhone] wp ON wp.UserId = u.Id
WHERE l.Id IN @lecturerIds;
";
        var queryResult = await connection.QueryAsync<LecturerPhone>(query, new { lecturerIds });

        return queryResult
            .GroupBy(x => x.Id)
            .ToDictionary(x => x.Key, x => x.Select(e => e.Phone).ToList());
    }

    public async Task<int> GetLecturersPagesCount(string? searchTerm)
    {
        using var connection = await _dbConnectionFactory.GetOpenStudentConnectionAsync();

        var search = GetSearchTerm(searchTerm);
        var query = @$"
SELECT COUNT(*)
FROM [Lecturer] l
INNER JOIN [User] u ON l.UserId = u.Id
WHERE @search IS NULL
        OR u.Name LIKE @search
        OR u.Surname LIKE @search
        OR CONCAT(u.Name, ' ', u.Surname) LIKE @search;
";

        return await connection.ExecuteScalarAsync<int>(
            query,
            new { search });
    }

    private string? GetSearchTerm(string? searchTerm) =>
        string.IsNullOrEmpty(searchTerm) ? null : $"%{searchTerm}%";
}
