using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

public class GameManager : MonoBehaviour
{
    public UserDB userDB;
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
    }

    public void UpdateUI()
    {
        username.text = userdata.userName;
        cash.text = string.Format("{0:N0} 원", userdata.cash);
        balance.text = string.Format("{0:N0} 원", userdata.balance);
    }

    public void SaveUserData()
    {
        //string json = JsonUtility.ToJson(userDB, true); //유니티
        string json = JsonConvert.SerializeObject(userDB, Formatting.Indented); // newton 기능 
        string path = Application.persistentDataPath + "/userdata.json";
        File.WriteAllText(path, json);
    }

    public void LoadUserData()
    {
        string path = Application.persistentDataPath + "/userdata.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            //userDB = JsonUtility.FromJson<UserDB>(json);
            userDB = JsonConvert.DeserializeObject<UserDB>(json);
        }
        else
        {
            userDB = new UserDB();
            userDB.userList.Add(new UserData("test01", "1234", "광섭", 100000, 50000));
            SaveUserData(); 
        }
    }
}

