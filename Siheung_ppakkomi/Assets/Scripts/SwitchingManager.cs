using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아두이노로 온값을 스위칭을 통해 버튼에 전달한다.
/// 버튼을 눌렀을 경우 아두이노로 값을 보낸다.
/// 보낸값을 스위칭을 통해 버튼에 나타낸다.
/// </summary>
public class SwitchingManager : MonoBehaviour
{

    public UILabel onLabel;
    public UILabel offLabel;
    public bool buttonState;

    void Start()
    {
        buttonState = true; //씬이 시작하면서 서버로 부터 상태를 여기에 입력한다.
        SwitchMethod();
        SwitchMethod();
    }

    public string SwitchMethod()
    {
        string result = "";


        onLabel.gameObject.SetActive(buttonState = !buttonState);
        offLabel.gameObject.SetActive(!buttonState);
        //Debug.Log(buttonState = !buttonState);       
        return result;
    }

    public enum ButtonState
    {
        on,
        off
    }
}
