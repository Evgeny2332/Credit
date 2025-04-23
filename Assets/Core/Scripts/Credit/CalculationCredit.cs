using System;
using UnityEngine;
using UnityEngine.UI;

public class CalculationCredit : MonoBehaviour
{
    private SupabaseConnector _connector;
    private User _user;

    [Header("Input")]
    [SerializeField] private InputField _amount;
    [SerializeField] private InputField _termMounts;

    [Header("Calculate")]
    [SerializeField] private Text _calculationData;

    [SerializeField] private int _interestRate;
    [SerializeField] private int _monthlyPayment;
    [SerializeField] private int _totalAmount;

    [Header("Windows")]
    [SerializeField] private GameObject _registerWindow;
    [SerializeField] private GameObject _doneCredit;
    [SerializeField] private GameObject _calculateWindow;

    private void Start()
    {
        _connector = new SupabaseConnector();
    }

    private void Update()
    {
        CheckInput();
    }

    public void Enable(User user)
    {
        _user = user;
        _calculateWindow.SetActive(true);
    }

    private void CheckInput()
    {
        if (!string.IsNullOrEmpty(_amount.text) && !string.IsNullOrEmpty(_termMounts.text))
        {
            if (int.TryParse(_amount.text, out int amount) && int.TryParse(_termMounts.text, out int termMounts))
            {
                if (amount < 100 || termMounts <= 0)
                {
                    _calculationData.text = "����� ������ ���� ������ 100, � ���� ������ ���� ������ 0.";
                    return;
                }

                _interestRate = CalculateInterestRate(amount);
                _monthlyPayment = MonthlyPayment(amount, termMounts, _interestRate);
                _totalAmount = _monthlyPayment * termMounts;

                ShowAllData();
            }
            else
                _calculationData.text = "������� ���������� �������� ��������.";
        }
        else
            _calculationData.text = "���� �� ������ ���� �������.";
    }

    private int CalculateInterestRate(int amount)
    {
        if (amount <= 10_000) return 50;
        else if (amount <= 1_000_000) return 30;
        else if (amount <= 10_000_000) return 20;

        return 0;
    }

    private int MonthlyPayment(int amount, int termMonths, int annualInterestRate)
    {
        decimal monthlyRate = (decimal)annualInterestRate / 12 / 100;

        if (monthlyRate == 0)
            return amount / termMonths;

        double rate = (double)monthlyRate;
        double factor = Math.Pow(1 + rate, termMonths);
        decimal annuityCoefficient = monthlyRate * (decimal)factor / ((decimal)factor - 1);
        decimal monthlyPayment = amount * annuityCoefficient;

        return (int)Math.Round(monthlyPayment);
    }

    private void ShowAllData()
    {
        _calculationData.text = $"���������� ������: {_interestRate}% �������" +
                                $"\n����������� �����: {_monthlyPayment} ���." +
                                $"\n����� ����� �� �������: {_totalAmount} ���.";
    }
    public void CreateApplication()
    {
        if (!int.TryParse(_amount.text, out int amount) ||
            !int.TryParse(_termMounts.text, out int termMonths) ||
            amount < 100 || termMonths <= 0)
        {
            Debug.LogWarning("������������ ������. ��������� ����� � ����.");
            return;
        }

        _doneCredit.SetActive(true);
        _registerWindow.SetActive(false);

        Loan newLoan = new Loan()
        {
            Amount = amount,
            TermMonths = termMonths,
            InterestRate = _interestRate,
            UserId = _user.id ?? 0,
            Status = "�� ������������"
        };

        _connector.AddLoanAsync(newLoan);

        Debug.Log("������ �������!");
    }

}
