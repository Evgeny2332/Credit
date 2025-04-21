using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class SupabaseConnector
{
    private const string baseUrl = "https://betrjvedpvjlvwxjpjrt.supabase.co/rest/v1/";
    private const string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJldHJqdmVkcHZqbHZ3eGpwanJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ5MDY3NzYsImV4cCI6MjA2MDQ4Mjc3Nn0.qTlRUGU1cNKp5uO0A9cnm5MZ3CdeSvVJhABR0Y8LQQ4";

    private static readonly HttpClient httpClient = new HttpClient();

    private async UniTask<List<T>> FetchTableAsync<T>(string tableName)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, baseUrl + tableName);
        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Debug.LogError($"ќшибка при запросе таблицы {tableName}: {response.StatusCode}");
            return null;
        }

        var json = await response.Content.ReadAsStringAsync();
        var items = JsonConvert.DeserializeObject<List<T>>(json);

        return items;
    }

    public async UniTask<bool> InsertRecordAsync<T>(string tableName, T record)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl + tableName);
        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");
        request.Headers.Add("Prefer", "return=minimal");

        string json = JsonConvert.SerializeObject(record);
        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Debug.LogError($"ќшибка при добавлении записи в {tableName}: {response.StatusCode} Ч {await response.Content.ReadAsStringAsync()}");
            return false;
        }

        return true;
    }


    public UniTask<List<User>> GetUsersAsync() => FetchTableAsync<User>("Users");
    public UniTask<List<UserAuthorizationData>> GetAuthorizationDataAsync() => FetchTableAsync<UserAuthorizationData>("UserAuthorizationData");
    public UniTask<List<Loan>> GetLoansAsync() => FetchTableAsync<Loan>("Loans");
    public UniTask<List<Contract>> GetContractsAsync() => FetchTableAsync<Contract>("Contracts");
    public UniTask<List<Role>> GetRolesAsync() => FetchTableAsync<Role>("Roles");

    public UniTask<bool> AddUserAsync(User user) => InsertRecordAsync("Users", user);

    public UniTask<bool> AddAuthorizationDataAsync(UserAuthorizationData authData) => InsertRecordAsync("UserAuthorizationData", authData);

    public UniTask<bool> AddLoanAsync(Loan loan) => InsertRecordAsync("Loans", loan);

    public UniTask<bool> AddContractAsync(Contract contract) => InsertRecordAsync("Contracts", contract);

    public UniTask<bool> AddRoleAsync(Role role) => InsertRecordAsync("Roles", role);

}
