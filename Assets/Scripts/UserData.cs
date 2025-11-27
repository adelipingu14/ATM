[System.Serializable]

public class UserData
{
    public string userId;
    public string password;

    public string userName;
    public int cash;
    public int balance;

    public UserData(string userId, string password,string userName, int cash, int balance)
    {
        this.userId = userId;
        this.password = password;

        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
    }
}
