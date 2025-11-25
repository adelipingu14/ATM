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
        userdata = new UserData("광섭", 100000, 50000);

        username.text = userdata.userName;
        cash.text = userdata.cash.ToString();
        balance.text = userdata.balance.ToString();
    }

    public void UpdateUI()
    {
        username.text = userdata.userName;
        cash.text = userdata.cash.ToString();
        balance.text = userdata.balance.ToString();
    }
}

