using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Stack<GameObject> uiStack = new Stack<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OpenUI(GameObject ui) //ui는 내가 열고싶은 ui창(x)
    {
        if (uiStack.Count > 0) //새로운 ui를 실행하고 이미 열린 ui창(y)이있다면(즉 아무것도 없는데 실행됐다면 상관없다)
        {
            uiStack.Peek().SetActive(false); // 이미 열린 ui창(y)을 끄고
        }

        ui.SetActive(true); // 내가 열고싶은 ui창(x)를 킨다
        uiStack.Push(ui);   // uiStack ui창(x) 가 맨위(혹은 맨뒤로) 들어가고 기존의 ui창(y)는 그 앞으로(혹은 그 아래로) 가게 된다
    }

    public void CloseTopUI() //ui창을 닫고싶다
    {
        if (uiStack.Count == 0) //아무것도 열린 ui창(y)이 없다면
            return; //아무것도 하지않는다

        GameObject top = uiStack.Pop(); // top은 맨위 OpenUI 기준 내가 열었던 ui창(x)고 이걸 pop(맨위에서 치움)한다
        top.SetActive(false);   //pop을통해 치워진 top(ui창(x))를 끈다

        if (uiStack.Count > 0)  // 아직 남아있는 ui가 있다면 즉 ui창(y) 가 있다면
        {
            uiStack.Peek().SetActive(true); // 그 ui창(y)를 킨다
        }
    }
}
