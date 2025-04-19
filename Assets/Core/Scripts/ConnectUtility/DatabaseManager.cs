using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private SupabaseConnector _connector;

    private void Awake()
    {
        _connector = new SupabaseConnector();
    }

    #region Пользователи

    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _connector.GetUsersAsync();
        return users;
    }

    public async Task<User> GetUserByIdAsync(int userId)
    {
        var users = await _connector.GetUsersAsync();
        return users?.Find(user => user.id == userId);
    }

    #endregion

    #region Кредиты

    public async Task<List<Loan>> GetLoansAsync()
    {
        var loans = await _connector.GetLoansAsync();
        return loans;
    }

    public async Task<Loan> GetLoanByIdAsync(int loanId)
    {
        var loans = await _connector.GetLoansAsync();
        return loans?.Find(loan => loan.id == loanId);
    }

    #endregion

    #region Контракты

    public async Task<List<Contract>> GetContractsAsync()
    {
        var contracts = await _connector.GetContractsAsync();
        return contracts;
    }

    public async Task<Contract> GetContractByIdAsync(int contractId)
    {
        var contracts = await _connector.GetContractsAsync();
        return contracts?.Find(contract => contract.id == contractId);
    }

    #endregion

    #region Роли

    public async Task<List<Role>> GetRolesAsync()
    {
        var roles = await _connector.GetRolesAsync();
        return roles;
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        var roles = await _connector.GetRolesAsync();
        return roles?.Find(role => role.id == roleId);
    }

    #endregion

    // Можно добавить методы для авторизации, если нужно
}
