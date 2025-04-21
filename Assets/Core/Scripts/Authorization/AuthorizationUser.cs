using UnityEngine;
using UnityEngine.UI;

public class AuthorizationUser : MonoBehaviour
{
    [SerializeField] private DatabaseManager _databaseManager;
    [SerializeField] private UserWindow _userWindow;
    [SerializeField] private AdminWindow _adminWindow;

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;

    [SerializeField] private GameObject _incorrectData;
    [SerializeField] private GameObject _authorizationWindow;

    [SerializeField] private int _userId;

    public async void LoginToAccount()
    {
        _incorrectData.SetActive(false);

        string login = _login.text;
        string password = _password.text;

        var authData = await _databaseManager.GetAuthorizationDataAsync();

        var user = authData?.Find(u => u.Login == login && u.Password == password);

        if (user != null)
        {
            _userId = user.id ?? 0;
            User newUser = await _databaseManager.GetUserByIdAsync(_userId);

            _authorizationWindow.SetActive(false);

            if (newUser.RoleId == 1)
                _userWindow.Enable(newUser);
            else if(newUser.RoleId == 2)
                _adminWindow.Enable(newUser);

            Debug.Log(newUser.RoleId);
        }
        else
        {
            _incorrectData.SetActive(true);
        }
    }
}
