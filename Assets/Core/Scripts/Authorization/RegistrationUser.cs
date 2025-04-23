using UnityEngine;
using UnityEngine.UI;

public class RegistrationUser : MonoBehaviour
{   
    private SupabaseConnector _connector;

    [SerializeField] private InputField _login;
    [SerializeField] private InputField _password;

    [SerializeField] private Text _messageText;

    private void Start()
    {
        _connector = new SupabaseConnector();
    }


    public async void CreateAccount()
    {
        if (string.IsNullOrWhiteSpace(_login.text) || string.IsNullOrWhiteSpace(_password.text))
        {
            ShowMessage("Поля не должны быть пустыми", Color.red);
            return;
        }

        string login = _login.text;
        string password = _password.text;

        if (await _connector.IsLoginTakenAsync(login))
        {
            ShowMessage("Логин уже занят", Color.red);
            return;
        }

        User user = new User() { RoleId = 1 };
        await _connector.AddUserAsync(user);

        UserAuthorizationData authorizationData = new UserAuthorizationData()
        {
            Login = login,
            Password = password,
        };
        await _connector.AddAuthorizationDataAsync(authorizationData);

        ShowMessage("Аккаунт создан. Воспользуйтесь окном входа", Color.green);
    }

    private void ShowMessage(string message, Color color)
    {
        _messageText.text = message;
        _messageText.color = color;
    }

}
