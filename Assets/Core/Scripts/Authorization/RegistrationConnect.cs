using UnityEngine;
using UnityEngine.UI;

public class RegistrationConnect : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _dataFetcher;

    [SerializeField] private InputField _loginInput;
    [SerializeField] private InputField _passwordInput;

    [SerializeField] private UserDataConnector _userConnector;
    [SerializeField] private GameObject _userWindow;
    [SerializeField] private GameObject _authorizationWindow;

    private User _user;

    public void Register()
    {
        Debug.Log("Начала регистрации");
        _user = new User()
        {
            FirstName = "",
            LastName = "",
            DateOfBirthday = "",
            PassportNumber = "",
            Phone = "",
            Address = "",
            RoleId = 1
        };

        StartCoroutine(_dataFetcher.SetUser(_user, Success, Error));

        UserAuthorizationData newUserAuthorization = new UserAuthorizationData()
        {
            Login = _loginInput.text,
            Password = _passwordInput.text,
        };

        StartCoroutine(_dataFetcher.SetUserAuthorizationData(newUserAuthorization, Success, Error));
        _userConnector.User = _user;
        _userConnector.CheckData();
        Debug.Log("Зарегались");
    }

    private void Success()
    {

        _authorizationWindow.SetActive(false);
        _userWindow.SetActive(true);
    }
    private void Error(string value)
    {
        Debug.Log(value);
    }
}
