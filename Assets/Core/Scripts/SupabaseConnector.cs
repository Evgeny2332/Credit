using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class SupabaseConnector : MonoBehaviour
{
    private string _projectUrl = "https://betrjvedpvjlvwxjpjrt.supabase.co";
    private string _apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImJldHJqdmVkcHZqbHZ3eGpwanJ0Iiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDQ5MDY3NzYsImV4cCI6MjA2MDQ4Mjc3Nn0.qTlRUGU1cNKp5uO0A9cnm5MZ3CdeSvVJhABR0Y8LQQ4";

    public IEnumerator ExecuteQuery(string query, System.Action<string> onSuccess, System.Action<string> onError)
    {
        string url = $"{_projectUrl}/rest/v1/{query}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("apikey", _apiKey);
        request.SetRequestHeader("Authorization", "Bearer " + _apiKey);
        request.SetRequestHeader("Accept", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke("Ошибка запроса: " + request.error);
        }
        else
        {
            onSuccess?.Invoke(request.downloadHandler.text);
        }
    }

    public IEnumerator SendData(string table, string jsonData, System.Action onSuccess, System.Action<string> onError)
    {
        string url = $"{_projectUrl}/rest/v1/{table}";
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonToSend);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("apikey", _apiKey);
        request.SetRequestHeader("Authorization", "Bearer " + _apiKey);
        request.SetRequestHeader("Prefer", "return=representation");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            onError?.Invoke("Ошибка отправки: " + request.error);
            Debug.Log("Ответ от сервера: " + request.downloadHandler.text);
        }
        else
        {
            onSuccess?.Invoke();
            Debug.Log("Ответ от сервера: " + request.downloadHandler.text);
        }
    }
}
    