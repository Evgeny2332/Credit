using System.Collections;
using UnityEngine;

public class SupabaseDataFetcher : MonoBehaviour
{
    [SerializeField] private SupabaseConnector _supabaseConnector;

    public IEnumerator GetUsers(System.Action<User[]> onSuccess, System.Action<string> onError)
    {
        string query = "Users";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            User[] users = JsonHelper.FromJson<User>(json);
            onSuccess?.Invoke(users);
        }, onError));
    }

    public IEnumerator SetUser(User user, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(user);
        yield return StartCoroutine(_supabaseConnector.SendData("Users", json, onSuccess, onError));
    }

    public IEnumerator GetLoans(System.Action<Loan[]> onSuccess, System.Action<string> onError)
    {
        string query = "Loans";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            Loan[] loans = JsonHelper.FromJson<Loan>(json);
            onSuccess?.Invoke(loans);
        }, onError));
    }

    public IEnumerator SetLoan(Loan loan, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(loan);
        yield return StartCoroutine(_supabaseConnector.SendData("Loans", json, onSuccess, onError));
    }

    public IEnumerator GetUserAuthorizationData(System.Action<UserAuthorizationData[]> onSuccess, System.Action<string> onError)
    {
        string query = "UserAuthorizationData";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            UserAuthorizationData[] authData = JsonHelper.FromJson<UserAuthorizationData>(json);
            onSuccess?.Invoke(authData);
        }, onError));
    }

    public IEnumerator SetUserAuthorizationData(UserAuthorizationData data, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(data);
        yield return StartCoroutine(_supabaseConnector.SendData("UserAuthorizationData", json, onSuccess, onError));
    }

    public IEnumerator GetContracts(System.Action<Contract[]> onSuccess, System.Action<string> onError)
    {
        string query = "Contracts";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            Contract[] contracts = JsonHelper.FromJson<Contract>(json);
            onSuccess?.Invoke(contracts);
        }, onError));
    }

    public IEnumerator SetContract(Contract contract, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(contract);
        yield return StartCoroutine(_supabaseConnector.SendData("Contracts", json, onSuccess, onError));
    }

    public IEnumerator GetRoles(System.Action<Role[]> onSuccess, System.Action<string> onError)
    {
        string query = "Roles";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            Role[] roles = JsonHelper.FromJson<Role>(json);
            onSuccess?.Invoke(roles);
        }, onError));
    }

    public IEnumerator SetRole(Role role, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(role);
        yield return StartCoroutine(_supabaseConnector.SendData("Roles", json, onSuccess, onError));
    }

    public IEnumerator GetPayments(System.Action<Payment[]> onSuccess, System.Action<string> onError)
    {
        string query = "Payments";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            Payment[] payments = JsonHelper.FromJson<Payment>(json);
            onSuccess?.Invoke(payments);
        }, onError));
    }

    public IEnumerator SetPayment(Payment payment, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(payment);
        yield return StartCoroutine(_supabaseConnector.SendData("Payments", json, onSuccess, onError));
    }

    public IEnumerator GetCreditProducts(System.Action<CreditProduct[]> onSuccess, System.Action<string> onError)
    {
        string query = "CreditProducts";
        yield return StartCoroutine(_supabaseConnector.ExecuteQuery(query, (json) =>
        {
            CreditProduct[] creditProducts = JsonHelper.FromJson<CreditProduct>(json);
            onSuccess?.Invoke(creditProducts);
        }, onError));
    }

    public IEnumerator SetCreditProduct(CreditProduct creditProduct, System.Action onSuccess, System.Action<string> onError)
    {
        string json = JsonUtility.ToJson(creditProduct);
        yield return StartCoroutine(_supabaseConnector.SendData("CreditProducts", json, onSuccess, onError));
    }
}
