using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UserData userdata;

    public Text username;
    public Text cash;
    public Text balance;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();

                if (instance == null)
                {
                    instance = new GameObject(nameof(GameManager)).AddComponent<GameManager>();
                    DontDestroyOnLoad(instance.gameObject);
                }
            }
            return instance;
        }
    }

    public void Start()
    {
        LoadUserData();

        username.text = userdata.userName;
        cash.text = string.Format("{0:N0} 원", userdata.cash);
        balance.text = string.Format("{0:N0} 원", userdata.balance);
    }

    public void UpdateUI()
    {
        username.text = userdata.userName;
        cash.text = string.Format("{0:N0} 원", userdata.cash);
        balance.text = string.Format("{0:N0} 원", userdata.balance);
    }

    public void SaveUserData()
    {
        PlayerPrefs.SetString("UserName", userdata.userName);
        PlayerPrefs.SetInt("Cash", userdata.cash);
        PlayerPrefs.SetInt("Balance", userdata.balance);

        PlayerPrefs.Save();
    }

    public void LoadUserData()
    {
        if (PlayerPrefs.HasKey("UserName"))
        {
            string name = PlayerPrefs.GetString("UserName");
            int cash = PlayerPrefs.GetInt("Cash");
            int balance = PlayerPrefs.GetInt("Balance");

            userdata = new UserData(name, cash, balance);
        }

        else
        {
            userdata = new UserData("광섭", 100000, 50000);
        }
    }
}

