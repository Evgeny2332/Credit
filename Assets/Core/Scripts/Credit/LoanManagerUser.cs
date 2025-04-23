using UnityEngine;

public class LoanManagerUser : MonoBehaviour
{
    [SerializeField] private GameObject _applicationsWindow;
    [SerializeField] private GameObject _loanSlotPrefab;
    [SerializeField] private Transform _loanSlotParent;

    private SupabaseConnector _connector;

    private void Awake()
    {
        _connector = new SupabaseConnector();
    }

    public void Enable(User user)
    {
        ClearLoanSlots();
        LoadUserLoans(user);
        _applicationsWindow.SetActive(true);
    }

    private async void LoadUserLoans(User user)
    {
        int userId = user.id ?? 0;
        var loans = await _connector.GetLoansAsync();
        var userLoans = loans?.FindAll(loan => loan.UserId == userId);

        if (userLoans != null && userLoans.Count > 0)
        {
            foreach (var loan in userLoans)
            {
                CreateLoanSlot(loan);
            }
        }
    }

    private void CreateLoanSlot(Loan loan)
    {
        var loanSlot = Instantiate(_loanSlotPrefab, _loanSlotParent);
        var applicationSlot = loanSlot.GetComponent<ApplicationSlot>();
        if (applicationSlot != null)
        {
            applicationSlot.Init(loan);
        }
    }

    private void ClearLoanSlots()
    {
        foreach (Transform child in _loanSlotParent)
        {
            Destroy(child.gameObject);
        }
    }
}
