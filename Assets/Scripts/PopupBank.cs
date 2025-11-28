using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{

    // 팝업 오브젝트 변수
    public GameObject deposit;
    public GameObject withDrawal;
    public GameObject atm;
    public GameObject login;
    public GameObject signin;
    public GameObject transfer;

    // inputField 전용 변수
    public InputField customDeposit;
    public InputField customWithDraw;

    public InputField loginId;
    public InputField loginPw;

    public InputField signInId;
    public InputField signInName;
    public InputField signInPw;
    public InputField signInPwConfirm;

    public InputField transferToName;
    public InputField transferamount;

    private void Start()
    {
        UIManager.Instance.OpenUI(login);

    }


    public void DepositOpen()
    {
        UIManager.Instance.OpenUI(deposit);
    }

    public void WithDrawalOpen()
    {
        UIManager.Instance.OpenUI(withDrawal);
    }


    public void Deposit(int amount)
    {
        var data = GameManager.Instance.userdata;

        if (data.cash >= amount)
        {
            data.cash -= amount;
            data.balance += amount;
            GameManager.Instance.UpdateUI();
            GameManager.Instance.SaveUserData();
        }
        Debug.Log("잔액이 부족합니다.");
    }

    public void WithDraw(int amount)
    {
        var data = GameManager.Instance.userdata;

        if (data.balance >= amount)
        {
            data.balance -= amount;
            data.cash += amount;
            GameManager.Instance.UpdateUI();
            GameManager.Instance.SaveUserData();
        }
        Debug.Log("잔액이 부족합니다.");
    }

    public void CustomDeposit()
    {
        string input = customDeposit.text;

        int amount;
        bool isInt = int.TryParse(input, out amount);

        if (!isInt)
        {
            return;
        }
        Deposit(amount);
    }

    public void CustomWithDraw()
    {
        string input = customWithDraw.text;

        int amount1;
        bool isInt = int.TryParse(input, out amount1);

        if (!isInt)
        {
            return;
        }
        WithDraw(amount1);
    }

    public void SignIn()
    {
        string id = signInId.text;
        string pw = signInPw.text;
        string pw2 = signInPwConfirm.text;
        string name = signInName.text;


        if (string.IsNullOrEmpty(id) ||
            string.IsNullOrEmpty(pw) ||
            string.IsNullOrEmpty(pw2) ||
            string.IsNullOrEmpty(name))
        {
            Debug.Log("잘못된 입력입니다."); // popup
            return;
        }


        if (pw != pw2)
        {
            Debug.Log("비밀번호가 다릅니다."); // popup
            return;
        }

        foreach (var user in GameManager.Instance.userDB.userList)
        {
            if (user.userId == id)
            {
                Debug.Log("이미 존재하는 아이디입니다."); // popup
                return;
            }
        }

        UserData newUser = new UserData(id, pw, name, 100000, 50000);
        GameManager.Instance.userDB.userList.Add(newUser);

        GameManager.Instance.userdata = newUser;

        GameManager.Instance.SaveUserData();
        GameManager.Instance.UpdateUI();

        Debug.Log("회원가입 완료!"); // popup

        UIManager.Instance.CloseTopUI();

    }

    public void TryLogin()
    {
        string id = loginId.text;
        string pw = loginPw.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
        {
            Debug.Log("잘못된 입력입니다.");  //popup
            return;
        }

        foreach (var user in GameManager.Instance.userDB.userList)
        {
            if (user.userId == id && user.password == pw)
            {
                Debug.Log("로그인 성공!");


                GameManager.Instance.userdata = user;

                GameManager.Instance.UpdateUI();
                loginId.text = null;
                loginPw.text = null;

                UIManager.Instance.CloseTopUI();
                UIManager.Instance.OpenUI(atm);



                return;
            }
        }

        Debug.Log("로그인 실패!"); //popup
    }

    public void Transfer()
    {
        string towho = transferToName.text;
        string sendAmountText = transferamount.text;

        UserData data = GameManager.Instance.userdata;

        int sendAmount;

        if (string.IsNullOrEmpty(towho) || string.IsNullOrEmpty(sendAmountText))
        {
            Debug.Log("잘못된 입력입니다.");
            return;
        }


        if (!int.TryParse(sendAmountText, out sendAmount))
            {
            Debug.Log("잘못된 입력입니다");
            return;
            }



        foreach (UserData user in GameManager.Instance.userDB.userList)
        {
            if (user.userName == towho)
            {
                Debug.Log("있어요! 399!");

                if (data.balance < sendAmount)
                {
                    Debug.Log("잔액이 부족합니다."); // popup
                    return;
                }

                if (data == user)
                {
                    Debug.Log("잘못된 입력입니다"); // popup
                    return;
                }

                data.balance -= sendAmount;

                user.balance += sendAmount;

                GameManager.Instance.SaveUserData();
                GameManager.Instance.UpdateUI();
                transferToName.text = "";
                transferamount.text = "";

                Debug.Log("송금 완료!"); // popup

                return;
            }
        }
    }

    public void LogOut()
    {
        GameManager.Instance.userdata = null;
        UIManager.Instance.CloseTopUI();
        UIManager.Instance.OpenUI(login);
    }
}
