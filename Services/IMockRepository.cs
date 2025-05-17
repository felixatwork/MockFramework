using MockFramework.Models;

namespace MockFramework.Services;

public interface IMockRepository {

    Task<MockResponse> GetMockAsync(string requestpath, string? requestpayload = null);

    Task<MockResponse> InsertMockAsync(string requestpath, string? requestpayload = null);

    Task<MockResponse> UpdateMockAsync(string requestpath, string? requestpayload = null);

    void DeleteMockAsync(string requestpath);
}