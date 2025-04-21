using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private SupabaseConnector _connector;

    private void Awake()
    {
        _connector = new SupabaseConnector();
    }

    #region Пользователи

    public async UniTask<List<User>> GetUsersAsync()
    {
        var users = await _connector.GetUsersAsync();
        return users;
    }

    public async UniTask<User> GetUserByIdAsync(int userId)
    {
        var users = await _connector.GetUsersAsync();
        return users?.Find(user => user.id == userId);
    }

    #endregion

    #region Кредиты

    public async UniTask<List<Loan>> GetLoansAsync()
    {
        var loans = await _connector.GetLoansAsync();
        return loans;
    }

    public async UniTask<Loan> GetLoanByIdAsync(int loanId)
    {
        var loans = await _connector.GetLoansAsync();
        return loans?.Find(loan => loan.id == loanId);
    }

    #endregion

    #region Контракты

    public async UniTask<List<Contract>> GetContractsAsync()
    {
        var contracts = await _connector.GetContractsAsync();
        return contracts;
    }

    public async UniTask<Contract> GetContractByIdAsync(int contractId)
    {
        var contracts = await _connector.GetContractsAsync();
        return contracts?.Find(contract => contract.id == contractId);
    }

    #endregion

    #region Роли

    public async UniTask<List<Role>> GetRolesAsync()
    {
        var roles = await _connector.GetRolesAsync();
        return roles;
    }

    public async UniTask<Role> GetRoleByIdAsync(int roleId)
    {
        var roles = await _connector.GetRolesAsync();
        return roles?.Find(role => role.id == roleId);
    }

    #endregion

    #region Авторизация

    public async UniTask<List<UserAuthorizationData>> GetAuthorizationDataAsync()
    {
        return await _connector.GetAuthorizationDataAsync();
    }

    public async UniTask<UserAuthorizationData> GetAuthorizationByCredentialsAsync(string login, string password)
    {
        var list = await GetAuthorizationDataAsync();
        return list?.Find(x => x.Login == login && x.Password == password);
    }

    #endregion
}
