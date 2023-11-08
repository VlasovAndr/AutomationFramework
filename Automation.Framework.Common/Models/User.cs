namespace Automation.Framework.Common.Models;

public class User
{
    public User(string fullName, string email, string currentAddress, string permanentAddress)
    {
        FullName = fullName;
        Email = email;
        CurrentAddress = currentAddress;
        PermanentAddress = permanentAddress;
    }

    public string PermanentAddress { get; }
    public string CurrentAddress { get; }
    public string Email { get; }
    public string FullName { get; }
}
