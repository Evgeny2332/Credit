using UnityEngine;
using UnityEngine.UI;

public class RegistrationUser : MonoBehaviour
{   
    private SupabaseConnector _connector;

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;

    [SerializeField] private UserWindow _userWindow;
    [SerializeField] private GameObject _registrationWindow;

    private void Start()
    {
        _connector = new SupabaseConnector();
    }

    public async void CreateAccount()
    {
        if (_login.text != "" && _password.text != "")
        {
            string login = _login.text;
            string password = _password.text;

            User user = new User()
            {
                RoleId = 1,
            };

            await _connector.AddUserAsync(user);

            UserAuthorizationData authorizationData = new UserAuthorizationData()
            {
                Login = login,
                Password = password,
            };

            await _connector.AddAuthorizationDataAsync(authorizationData);

            _registrationWindow.SetActive(false);
            _userWindow.Enable(user);
        }
    }
}
