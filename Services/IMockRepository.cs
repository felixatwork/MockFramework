using MockFramework.Models;

namespace MockFramework.Services;

public interface IMockRepository {

    Task<MockResponses> GetMockAsync(string requestpath, string requestpayload);
}