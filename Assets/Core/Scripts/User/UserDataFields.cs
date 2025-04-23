using UnityEngine;
using UnityEngine.UI;

public class UserDataFields : MonoBehaviour
{
    private SupabaseConnector _connector;

    [SerializeField] private InputField _firstName;
    [SerializeField] private InputField _lastName;
    [SerializeField] private InputField _dateOfBirthday;
    [SerializeField] private InputField _seriesPassport;
    [SerializeField] private InputField _numberPassport;
    [SerializeField] private InputField _phoneNumber;
    [SerializeField] private InputField _addressRegistration;

    [SerializeField] private GameObject _dataFields;
    [SerializeField] private GameObject _controlButtons;

    private User _user;

    private void Start()
    {
        _connector = new SupabaseConnector();   
    }

    public void Enable(User user)
    {
        _user = user;
        _dataFields.SetActive(true);
    }

    public void SaveData()
    {
        if (string.IsNullOrWhiteSpace(_firstName.text) ||
            string.IsNullOrWhiteSpace(_lastName.text) ||
            string.IsNullOrWhiteSpace(_dateOfBirthday.text) ||
            string.IsNullOrWhiteSpace(_seriesPassport.text) ||
            string.IsNullOrWhiteSpace(_numberPassport.text) ||
            string.IsNullOrWhiteSpace(_phoneNumber.text) ||
            string.IsNullOrWhiteSpace(_addressRegistration.text))
        {
            return;
        }

        User newUser = new User()
        {
            id = _user.id,
            FirstName = _firstName.text,
            LastName = _lastName.text,
            DateOfBirthday = _dateOfBirthday.text,
            PassportNumber = $"{_seriesPassport.text} {_numberPassport.text}",
            Phone = _phoneNumber.text,
            Address = _addressRegistration.text,
            RoleId = 1
        };

        _connector.UpdateUserAsync(newUser);

        _dataFields.SetActive(false);
        _controlButtons.SetActive(true);
    }
}
