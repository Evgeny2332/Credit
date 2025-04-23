using UnityEngine;
using UnityEngine.UI;

public class ApplicationSlotAdmin : MonoBehaviour
{
    [SerializeField] private Text _applicationNumber;
    [SerializeField] private Text _amount;
    [SerializeField] private Text _termMounts;
    [SerializeField] private Text _rate;

    private SupabaseConnector _connector;
    private Loan _loan;

    public void Init(Loan loan)
    {
        _connector = new SupabaseConnector();
        _loan = loan;

        if (loan != null)
            PrintLoanData(loan);
    }

    private void PrintLoanData(Loan loan)
    {
        _applicationNumber.text = $"Заявка номер: {loan.id.ToString()}";
        _amount.text = $"Сумма: {loan.Amount.ToString()}";
        _termMounts.text = $"Срок (мес.): {loan.TermMonths.ToString()}";
        _rate.text = $"Процентная ставка: {loan.InterestRate.ToString()}";
    }

    public void Accept()
    {
        Contract newContract = new Contract()
        {
            LoanId = _loan.id ?? 0,
            Terms = _loan.TermMonths.ToString(),
        };
        _connector.AddContractAsync(newContract);

        Loan loan = new Loan()
        {
            id = _loan.id ?? 0,
            Amount = _loan.Amount,
            InterestRate = _loan.InterestRate,
            TermMonths = _loan.TermMonths,
            UserId = _loan.UserId,
            Status = "Одобрено",
        };
        _connector.UpdateLoanAsync(loan);
        Destroy(gameObject);
    }
    public void Cancel()
    {
        Loan loan = new Loan()
        {
            id = _loan.id ?? 0,
            Amount = _loan.Amount,
            InterestRate = _loan.InterestRate,
            TermMonths = _loan.TermMonths,
            UserId = _loan.UserId,
            Status = "Отклонено",
        };
        _connector.UpdateLoanAsync(loan);
        Destroy(gameObject);
    }
}
