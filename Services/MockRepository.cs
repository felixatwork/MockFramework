
using System.Data;
using Dapper;
using MockFramework.Models;

namespace MockFramework.Services;

public class MockRepository : IMockRepository {

    private readonly IDbConnection _db;

    public MockRepository(IDbConnection db)
    {
        _db = db;
    }

    public async void DeleteMockAsync(string requestpath)
    {
        if (requestpath != null) {
            var sql = "DELETE FROM Mock where requestpath = '" + requestpath + "'";

            await _db.QueryAsync(sql);
        };
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

    public void InsertMockAsync(string requestpath, string? requestpayload = null)
    {
        throw new NotImplementedException();
    }

    public void UpdateMockAsync(string requestpath, string? requestpayload = null)
    {
        throw new NotImplementedException();
    }
}
