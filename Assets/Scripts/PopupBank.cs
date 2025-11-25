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
        }
    }

    public void WithDraw(int amount)
    {
        if (_userData.balance >= amount)
        {
            _userData.balance -= amount;
            _userData.cash += amount;
            GameManager.Instance.UpdateUI();
        }
    }

    public void CustomDeposit()
    {
        string input = customDeposit.text;

        int amount;
        bool isInt = int.TryParse(input, out amount);

        if (!isInt)
        {
            Debug.Log("숫자가 아닙니다.");
            return;
        }
        Deposit(amount);
    }

    public void CustomWithDraw()
    {
        int amount = int.Parse(customWithDraw.text);
        WithDraw(amount);
    }

}
