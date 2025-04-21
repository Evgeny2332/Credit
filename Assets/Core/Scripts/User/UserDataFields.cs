using UnityEngine;
using UnityEngine.UI;

public class UserDataFields : MonoBehaviour
{
    [SerializeField] private DatabaseManager _databaseManager;

    [SerializeField] private InputField _firstName;
    [SerializeField] private InputField _lastName;
    [SerializeField] private InputField _dateOfBirthday;
    [SerializeField] private InputField _seriesPassport;
    [SerializeField] private InputField _numberPassport;
    [SerializeField] private InputField _phoneNumber;
    [SerializeField] private InputField _addressRegistration;

    [SerializeField] private GameObject _dataFields;

    public void SaveData()
    {
    }
}
