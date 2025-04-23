using UnityEngine;

public class UserWindow : MonoBehaviour
{
    [SerializeField] private GameObject _userWindow;
    [SerializeField] private GameObject _controlButtons;

    [SerializeField] private UserDataFields _dataFields;
    [SerializeField] private CalculationCredit _calculationCredit;
    [SerializeField] private LoanManagerUser _loanManagerUser;


    private User _user;

    public void Enable(User user)
    {
        _user = user;

        if (user.FirstName != null)
        {
            _controlButtons.SetActive(true);
        }
        else
        {
            _dataFields.Enable(user);
        }

        _userWindow.SetActive(true);
    }

    public void OpenRegisterCreditWindow()
    {
        _controlButtons.SetActive(false);
        _calculationCredit.Enable(_user);
    }

    public void OpenApplicationsWindow()
    {
        _controlButtons.SetActive(false);
        _loanManagerUser.Enable(_user);
    }
}
