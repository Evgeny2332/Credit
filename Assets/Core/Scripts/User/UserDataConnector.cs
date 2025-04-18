using UnityEngine;

public class UserDataConnector : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _fetcher;

    [SerializeField] private GameObject _dataFields;

    [SerializeField] private bool _isRunStart;

    public void CheckData(User user)
    {
        if (user.FirstName != null || user.FirstName != "") 
            _dataFields.SetActive(false);
    }

    private void OnError(string error)
    {
        Debug.Log(error);
    }
}
