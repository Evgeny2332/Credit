using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistrationConnect : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _dataFetcher;

    [SerializeField] private InputField _loginInput;
    [SerializeField] private InputField _passwordInput;

    [SerializeField] private UserDataConnector _userConnector;

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
            Income = 0,
            RoleId = 1
        };

        StartCoroutine(_dataFetcher.SetUser(_user, Success, Error));

        UserAuthorizationData newUserAuthorization = new UserAuthorizationData()
        {
            Login = _loginInput.text,
            Password = _passwordInput.text,
        };

        StartCoroutine(_dataFetcher.SetUserAuthorizationData(newUserAuthorization, Success, Error));
        Debug.Log("Зарегались");
    }

    private void Success()
    {
        
        SceneManager.LoadScene(1);
    }
    private void Error(string value)
    {
        Debug.Log(value);
    }
}
