using MockFramework.Models;

namespace MockFramework.Services;

public interface IMockRepository {

    Task<MockResponse> GetMockAsync(string requestpath, string? requestpayload = null);

    void InsertMockAsync(MockCreateDto mockCreateDto);

    void DeleteMockAsync(string requestpath);
}