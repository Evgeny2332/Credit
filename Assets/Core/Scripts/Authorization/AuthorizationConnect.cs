using UnityEngine;
using UnityEngine.UI;

public class AuthorizationConnect : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _dataFetcher;
    [SerializeField] private UserDataConnector _userConnector;

    [SerializeField] private InputField _loginInput;
    [SerializeField] private InputField _passwordInput;

    [SerializeField] private GameObject _incorrectText;

    [SerializeField] private GameObject _userWindow;
    [SerializeField] private GameObject _adminWindow;

    [SerializeField] private GameObject _authorizationWindow;

    private User _user;
    private int _userId;

    public void LoginToAccount()
    {
        StartCoroutine(_dataFetcher.GetUserAuthorizationData(CheckAuthorizationData, ErrorConnect));
    }

    private void CheckAuthorizationData(UserAuthorizationData[] userAuthorizationData)
    {
        string login = _loginInput.text;
        string password = _passwordInput.text;
        _incorrectText.SetActive(false);

        foreach (UserAuthorizationData data in userAuthorizationData)
        {
            if (data.Login == login && data.Password == password)
            {
                _userId = data.UserId;
                StartCoroutine(_dataFetcher.GetUsers(LoadUserData, ErrorConnect));
                return;
            }
        }

        _incorrectText.SetActive(true);
    }

    private void LoadUserData(User[] users)
    {
        foreach (var user in users)
        {
            if (user.id == _userId)
            {
                _user = user;
                _userConnector.User = _user;

                if(user.RoleId == 1)
                    _userWindow.SetActive(true);
                else if(user.RoleId == 2)
                    _adminWindow.SetActive(true);

                Debug.Log(user.RoleId);

                _authorizationWindow.SetActive(false);

                _userConnector.CheckData();
                return;
            }
        }
    }

    private void ErrorConnect(string error)
    {
        Debug.Log($"При загрузке данных авторизации произошла ошибка: {error}");
    }
}
