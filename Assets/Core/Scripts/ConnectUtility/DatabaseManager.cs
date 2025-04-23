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
        return await _connector.GetUsersAsync();
    }

    public async UniTask<User> GetUserByIdAsync(int userId)
    {
        var users = await _connector.GetUsersAsync();
        return users?.Find(user => user.id == userId);
    }

    public async UniTask<bool> UpdateUserAsync(User user)
    {
        return await _connector.UpdateUserAsync(user);
    }

    #endregion

    #region Кредиты

    public async UniTask<List<Loan>> GetLoansAsync()
    {
        return await _connector.GetLoansAsync();
    }

    public async UniTask<Loan> GetLoanByIdAsync(int loanId)
    {
        var loans = await _connector.GetLoansAsync();
        return loans?.Find(loan => loan.id == loanId);
    }

    public async UniTask<bool> UpdateLoanAsync(Loan loan)
    {
        return await _connector.UpdateLoanAsync(loan);
    }

    #endregion

    #region Контракты

    public async UniTask<List<Contract>> GetContractsAsync()
    {
        return await _connector.GetContractsAsync();
    }

    public async UniTask<Contract> GetContractByIdAsync(int contractId)
    {
        var contracts = await _connector.GetContractsAsync();
        return contracts?.Find(contract => contract.id == contractId);
    }

    public async UniTask<bool> UpdateContractAsync(Contract contract)
    {
        return await _connector.UpdateContractAsync(contract);
    }

    #endregion

    #region Роли

    public async UniTask<List<Role>> GetRolesAsync()
    {
        return await _connector.GetRolesAsync();
    }

    public async UniTask<Role> GetRoleByIdAsync(int roleId)
    {
        var roles = await _connector.GetRolesAsync();
        return roles?.Find(role => role.id == roleId);
    }

    public async UniTask<bool> UpdateRoleAsync(Role role)
    {
        return await _connector.UpdateRoleAsync(role);
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

    public async UniTask<bool> UpdateAuthorizationDataAsync(UserAuthorizationData data)
    {
        return await _connector.UpdateAuthorizationDataAsync(data);
    }

    #endregion
}
