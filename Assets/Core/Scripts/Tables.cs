using System;

[System.Serializable]
public class User
{
    public int id;
    public string FirstName;
    public string LastName;
    public string DateOfBirthday;
    public string PassportNumber;
    public string Phone;
    public string Address;
    public int RoleId;
}

[System.Serializable]
public class Loan
{
    public int LoanId;
    public int UserId;
    public int CreditProductId;
    public int Amount;
    public int TermMonths;
    public int InterestRate;
    public string StartDate;
    public string Status;
}

[System.Serializable]
public class UserAuthorizationData
{
    public int DataId;
    public string Login;
    public string Password;
    public int UserId;
}

[System.Serializable]
public class Contract
{
    public int ContractId;
    public int LoanId;
    public string ContractDate; 
    public string ExpiryDate; 
    public string Terms;
}

[System.Serializable]
public class Role
{
    public int RoleId;
    public string Name;
}

[System.Serializable]
public class Payment
{
    public int PaymentId;
    public int LoanId;
    public string PaymentDate;
    public int Amount;
}

[System.Serializable]
public class CreditProduct
{
    public int CreditProductId;
    public string Name;
    public int InterestRate;
    public int MaxAmount;
    public int MaxTermMonths;
}
