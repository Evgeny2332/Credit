using UnityEngine;

public class AdminWindow : MonoBehaviour
{
    [SerializeField] private GameObject _adminWindow;
    [SerializeField] private GameObject _controlButtons;

    private User _user;

    public void Enable(User user)
    {
        _user = user;

        if (user.FirstName != null && user.FirstName != "")
        {
            _adminWindow.SetActive(true);
            _controlButtons.SetActive(true);
        }
    }
}
