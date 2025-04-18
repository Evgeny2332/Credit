using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthorizationConnect : MonoBehaviour
{
    [SerializeField] private SupabaseDataFetcher _dataFetcher;

    [SerializeField] private InputField _loginInput;
    [SerializeField] private InputField _passwordInput;

    [SerializeField] private GameObject _incorrectText;

    public void LoginToAccount()
    {
        StartCoroutine(_dataFetcher.GetUserAuthorizationData(CheckData, ErrorConnect));
    }

    private void CheckData(UserAuthorizationData[] userAuthorizationData)
    {
        string login = _loginInput.text;
        string password = _passwordInput.text;
        _incorrectText.SetActive(false);

        Debug.Log("������� ������� ������");
        foreach (UserAuthorizationData data in userAuthorizationData)
        {
            if (data.Login == login && data.Password == password)
            {
                ConnectData();
                Debug.Log("������ �����");
                return;
            }
        }
        Debug.Log("������ ���");
        _incorrectText.SetActive(true);
    }

    private void ErrorConnect(string error)
    {
        Debug.Log($"��� �������� ������ ����������� ��������� ������: {error}");
    }

    private void ConnectData()
    {
        SceneManager.LoadScene(1);
    }
}
