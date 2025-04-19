using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class SupabaseConnector
{
    private const string baseUrl = "https://your-project.supabase.co/rest/v1/";
    private const string apiKey = "your-anon-key";

    private static readonly HttpClient httpClient = new HttpClient();

    private async Task<List<T>> FetchTableAsync<T>(string tableName)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + tableName);
        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Debug.LogError($"Ошибка при запросе таблицы {tableName}: {response.StatusCode}");
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var items = JsonConvert.DeserializeObject<List<T>>(json);
        return items;
    }

    public Task<List<User>> GetUsersAsync() => FetchTableAsync<User>("User");
    public Task<List<UserAuthorizationData>> GetAuthDataAsync() => FetchTableAsync<UserAuthorizationData>("UserAuthorizationData");
    public Task<List<Loan>> GetLoansAsync() => FetchTableAsync<Loan>("Loan");
    public Task<List<Contract>> GetContractsAsync() => FetchTableAsync<Contract>("Contract");
    public Task<List<Role>> GetRolesAsync() => FetchTableAsync<Role>("Role");
}
