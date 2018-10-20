using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    private void OnClick()
    {
        if (transform.name == "Button - Exit")
            Application.Quit();
        if (transform.name == "Button_Moter_1")
            GetComponent<SwitchingManager>().SwitchMethod();
    }
}
