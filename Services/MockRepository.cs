
using System.Data;
using Dapper;
using MockFramework.Models;

namespace MockFramework.Services;

public class MockRepository(IDbConnection db) : IMockRepository {

    private readonly IDbConnection _db = db;

    private readonly ILogger _logger;

    public async void DeleteMockAsync(string requestpath)
    {
        if (requestpath != null)
        {
            var sql = "DELETE FROM Mock where requestpath = '" + requestpath + "'";

            int rowsAffected = await _db.ExecuteAsync(sql);

            if (rowsAffected > 0) {
                _logger.LogDebug("Record removed");
            }
        }
        ;
    }

    public async Task<MockResponse> GetMockAsync(string requestpath, string? requestpayload = null)
    {
        var sql = "SELECT requestpath, requestpayload, responsepayload, delay, httpstatuscode FROM Mock where requestpath = '" + requestpath + "'";

        if (requestpayload != null) {
            sql = $"{sql} and requestpayload = '{requestpayload}";
        };

        var result = await _db.QueryAsync<Mock>(sql);

        return new MockResponse
        {
            Payload = result.FirstOrDefault().ResponsePayload,
            DelayTime = (int)result.FirstOrDefault().Delay,
            HttpStatusCode = (int)result.FirstOrDefault().HttpStatusCode
        };
    }

    public async void InsertMockAsync(MockCreateDto mockCreateDto)
    {
        if (mockCreateDto != null)
        {
            var sql = @"
            INSERT INTO mock (requestpath, requestpayload, responsepayload, delay, httpstatuscode)
            VALUES (@RequestPath, @RequestPayload, @ResponsePayload, @Delay, @HttpStatusCode);";

            int rowsAffected = await _db.ExecuteAsync(sql);

            if (rowsAffected > 0) {
                _logger.LogDebug("New entry recorded");
            }
        }
    }
}
