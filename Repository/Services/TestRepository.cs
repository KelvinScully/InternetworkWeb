using ACommon.Objects;
using Common;
using Repository.Interfaces;
using Repository.Objects;
using System.Net.Http.Json;

namespace Repository.Services
{
    internal class TestRepository: RepositoryBase, ITestRepository
    {
        public TestRepository(ConnectionOptions options) : base(options)
        { }

        public async Task<ApiResult<TestItem>> TestItemGet(int testItemId)
        {
            try
            {
                string endpoint = $"/V1/Test/Item/{testItemId}";
                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<TestItem>
                    {
                        IsSuccessful = false,
                        Message = $"Failed with status code: {response.StatusCode}"
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<ApiResult<TestItem>>();
                return result ?? new ApiResult<TestItem> { IsSuccessful = false, Message = "Empty result" };
            }
            catch (Exception ex)
            {
                return new ApiResult<TestItem>
                {
                    IsSuccessful = false,
                    Message = $"Exception: {ex.Message}"
                };
            }
        }
    }
}