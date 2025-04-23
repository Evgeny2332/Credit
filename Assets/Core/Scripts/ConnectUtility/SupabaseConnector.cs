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
        return JsonConvert.DeserializeObject<List<T>>(json);
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

    public async UniTask<bool> UpdateRecordAsync<T>(string tableName, int id, T updatedRecord)
    {
        var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"{baseUrl}{tableName}?id=eq.{id}");
        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");
        request.Headers.Add("Prefer", "return=minimal");

        string json = JsonConvert.SerializeObject(updatedRecord);
        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Debug.LogError($"ќшибка при обновлении записи в {tableName} (id={id}): {response.StatusCode} Ч {await response.Content.ReadAsStringAsync()}");
            return false;
        }

        return true;
    }

    public async UniTask<bool> IsLoginTakenAsync(string login)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}UserAuthorizationData?Login=eq.{login}");
        request.Headers.Add("apikey", apiKey);
        request.Headers.Add("Authorization", $"Bearer {apiKey}");

        var response = await httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            Debug.LogError($"ќшибка при проверке логина: {response.StatusCode}");
            return false;
        }

        var json = await response.Content.ReadAsStringAsync();
        var records = JsonConvert.DeserializeObject<List<UserAuthorizationData>>(json);

        return records.Count > 0;
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

    public UniTask<bool> UpdateUserAsync(User user) => UpdateRecordAsync("Users", user.id ?? 0, user);
    public UniTask<bool> UpdateAuthorizationDataAsync(UserAuthorizationData data) => UpdateRecordAsync("UserAuthorizationData", data.id ?? 0, data);
    public UniTask<bool> UpdateLoanAsync(Loan loan) => UpdateRecordAsync("Loans", loan.id ?? 0, loan);
    public UniTask<bool> UpdateContractAsync(Contract contract) => UpdateRecordAsync("Contracts", contract.id ?? 0, contract);
    public UniTask<bool> UpdateRoleAsync(Role role) => UpdateRecordAsync("Roles", role.id ?? 0, role);
}
