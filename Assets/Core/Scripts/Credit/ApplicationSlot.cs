using UnityEngine;
using UnityEngine.UI;

public class ApplicationSlot : MonoBehaviour
{
    [SerializeField] private Text _applicationNumber;
    [SerializeField] private Text _amount;
    [SerializeField] private Text _termMounts;
    [SerializeField] private Text _rate;
    [SerializeField] private Text _status;

    public void Init(Loan loan)
    {
        if (loan != null)
            PrintLoanData(loan);
    }

    private void PrintLoanData(Loan loan)
    {
        _applicationNumber.text = $"Заявка номер: {loan.id.ToString()}";
        _amount.text = $"Сумма: {loan.Amount.ToString()}";
        _termMounts.text = $"Срок (мес.): {loan.TermMonths.ToString()}";
        _rate.text = $"Процентная ставка: {loan.InterestRate.ToString()}";
        _status.text = loan.Status;

        switch (loan.Status)
        {
            case "На рассмотрении":
                _status.color = Color.yellow;
                break;
            case "Отклонено":
                _status.color = Color.red;
                break;
            case "Одобрено":
                _status.color = Color.green;
                break;
            default:
                _status.color = Color.white; // Если статус неизвестен
                break;
        }
    }
}
