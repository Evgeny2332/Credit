using UnityEngine;

public class LoanManagerAdmin : MonoBehaviour
{
    [SerializeField] private GameObject _applicationsWindow;
    [SerializeField] private GameObject _loanSlotPrefab;
    [SerializeField] private Transform _loanSlotParent;

    private SupabaseConnector _connector;

    private void Awake()
    {
        _connector = new SupabaseConnector();
    }

    public void Enable()
    {
        ClearLoanSlots();
        LoadPendingLoans();
        _applicationsWindow.SetActive(true);
    }

    private async void LoadPendingLoans()
    {
        var loans = await _connector.GetLoansAsync();
        var pendingLoans = loans?.FindAll(loan => loan.Status == "На рассмотрении");

        if (pendingLoans != null && pendingLoans.Count > 0)
        {
            foreach (var loan in pendingLoans)
            {
                CreateLoanSlot(loan);
            }
        }
    }

    private void CreateLoanSlot(Loan loan)
    {
        var loanSlot = Instantiate(_loanSlotPrefab, _loanSlotParent);
        var applicationSlot = loanSlot.GetComponent<ApplicationSlotAdmin>();
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
