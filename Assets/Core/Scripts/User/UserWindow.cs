using UnityEngine;

public class UserWindow : MonoBehaviour
{
    [SerializeField] private GameObject _userWindow;
    [SerializeField] private GameObject _dataFields;
    [SerializeField] private GameObject _controlButtons;

    private User _user;

    public void Enable(User user)
    {
        _user = user;

        if (user.FirstName != null)
        {
            _dataFields.SetActive(false);
            _controlButtons.SetActive(true);
        }
        else
        {
            _dataFields.SetActive(true);
        }

        _userWindow.SetActive(true);
    }

}
