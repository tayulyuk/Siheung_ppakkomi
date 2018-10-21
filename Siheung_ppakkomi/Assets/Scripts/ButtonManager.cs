using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public bool pingPong;

    void Start()
    {
        pingPong = false;
    }
    private void OnClick()
    {
        if (transform.name == "Button - Exit")
            Application.Quit();
        if (transform.name == "Button_Moter_1")
            GetComponent<SwitchingManager>().SwitchMethod();
        if (transform.name == "Button_Moter_2")
            GetComponent<SwitchingManager>().SwitchMethod();
        if (transform.name == "Button_Moter_3")
            GetComponent<SwitchingManager>().SwitchMethod();
        if (transform.name == "Button_Moter_4")
            GetComponent<SwitchingManager>().SwitchMethod();
        if (transform.name == "Button_Power")
        {
            GetComponent<SwitchingManager>().SwitchMethod();
            GetComponent<PowerButtonManager>().MoterButtonState(pingPong = !pingPong);
        }
            
    }
}
