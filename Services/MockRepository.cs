
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

    public async Task<MockResponses> GetMockAsync(string requestpath, string requestpayload)
    {
        var sql = ("SELECT requestpath, requestpayload, responsepayload, delay, httpstatuscode FROM Mock " +
        "where requestpath = '") + requestpath + "' and requestpayload = '" + requestpayload + "";
        
        var result = await _db.QueryAsync<Mock>(sql);

        return new MockResponses
        {
            Payload = result.FirstOrDefault().ResponsePayload,
            DelayTime = (int)result.FirstOrDefault().Delay
        };
    }
}
