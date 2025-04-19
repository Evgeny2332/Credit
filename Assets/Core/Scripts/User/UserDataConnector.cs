using UnityEngine;

public class UserDataConnector : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _fetcher;
    [SerializeField] private UserDataValidation _userDataValidation;

    [SerializeField] private GameObject _dataFields;
    [SerializeField] private GameObject _ControllButtons;

    [HideInInspector] public User User;

    public void CheckData()
    {
        if (User.FirstName != null && User.FirstName != "")
        {
            _dataFields.SetActive(false);
            _ControllButtons.SetActive(true);
        }

        _userDataValidation.IdUser = User.id;
    }
}
