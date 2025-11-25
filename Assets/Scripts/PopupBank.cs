using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    private UserData _userData;

    public GameObject deposit;
    public GameObject withDrawal;
    public GameObject atm;
    public InputField customDeposit;
    public InputField customWithDraw;


    private GameObject currentPopUp;

    
    
    

    private void Start()
    {
        _userData = GameManager.Instance.userdata;
        
    }


    public void DepositOpen()
    {
        atm.SetActive(false);
        deposit.SetActive(true);
        currentPopUp = deposit;
    }

    public void WithDrawalOpen()
    {
        atm.SetActive(false);
        withDrawal.SetActive(true);
        currentPopUp = withDrawal;
    }

    public void CloseCurrentPopup()
    { 
        currentPopUp.SetActive(false);
        currentPopUp = null;
        atm.SetActive(true);
    }

    public void Deposit(int amount)
    {
        if (_userData.cash >= amount)
        {
            _userData.cash -= amount;
            _userData.balance += amount;
            GameManager.Instance.UpdateUI();
            GameManager.Instance.SaveUserData();
        }
    }

    public void WithDraw(int amount)
    {
        if (_userData.balance >= amount)
        {
            _userData.balance -= amount;
            _userData.cash += amount;
            GameManager.Instance.UpdateUI();
            GameManager.Instance.SaveUserData();
        }
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

}
