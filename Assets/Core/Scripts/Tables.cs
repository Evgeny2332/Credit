using System;
using Newtonsoft.Json;

[System.Serializable]
public class User
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DateOfBirthday { get; set; }
    public string PassportNumber { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int RoleId { get; set; }
}

[System.Serializable]
public class Loan
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? id { get; set; }

    public int UserId { get; set; }
    public int Amount { get; set; }
    public int TermMonths { get; set; }
    public int InterestRate { get; set; }
    public string StartDate { get; set; }
    public string Status { get; set; }
}

[System.Serializable]
public class UserAuthorizationData
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? id { get; set; }

    public string Login { get; set; }
    public string Password { get; set; }
}

[System.Serializable]
public class Contract
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? id { get; set; }

    public int LoanId { get; set; }
    public string ContractDate { get; set; }
    public string ExpiryDate { get; set; }
    public string Terms { get; set; }
}

[System.Serializable]
public class Role
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? id { get; set; }

    public string Name { get; set; }
}
