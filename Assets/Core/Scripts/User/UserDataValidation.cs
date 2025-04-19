using UnityEngine;
using UnityEngine.UI;

public class UserDataValidation : MonoBehaviour
{
    [SerializeField] private InputField _firstName;
    [SerializeField] private InputField _lastName;
    [SerializeField] private InputField _dateOfBirthday;
    [SerializeField] private InputField _seriesPassport;
    [SerializeField] private InputField _numberPassport;
    [SerializeField] private InputField _phoneNumber;
    [SerializeField] private InputField _addressRegistration;

    [SerializeField] private SupabaseDataFetcher _dataFetcher;

    [HideInInspector] public int IdUser;

    public void SaveData()
    {
        User newUser = new User()
        {
            FirstName = _firstName.text,
            LastName = _lastName.text,
            DateOfBirthday = _dateOfBirthday.text,
            PassportNumber = $"{_seriesPassport.text} {_numberPassport.text}",
            Phone = _phoneNumber.text,
            Address = _addressRegistration.text,
            RoleId = 1
        };

        StartCoroutine(_dataFetcher.UpdateUser(IdUser, newUser, OnSuccess, OnError));
    }

    private void OnSuccess()
    {
        Debug.Log("Данные загружены");
    }
    private void OnError(string error)
    {
        Debug.Log(error);
    }
}
