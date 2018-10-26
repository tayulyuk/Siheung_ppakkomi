using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public bool pingPong;

    void Start()
    {
        pingPong = false;
        SendAllButtonSetting();
    }
    private void OnClick()
    {
        if (transform.name == "Button - Exit")
            Application.Quit();
        if (transform.name == "Button_Moter_1")
            GetComponent<SwitchingManager>().SwitchMethod((GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_1_State == "1") ? true : false);
        if (transform.name == "Button_Moter_2")
            GetComponent<SwitchingManager>().SwitchMethod((GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_2_State == "1") ? true : false);
        if (transform.name == "Button_Moter_3")
            GetComponent<SwitchingManager>().SwitchMethod((GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_3_State == "1") ? true : false);
        if (transform.name == "Button_Moter_4")
            GetComponent<SwitchingManager>().SwitchMethod((GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_4_State == "1") ? true : false);
        if (transform.name == "Button_Power")
        {
            GetComponent<SwitchingManager>().SwitchMethod((GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().PowerButtonState == "1") ? true : false);
            GetComponent<PowerButtonManager>().MoterButtonState(pingPong = !pingPong);
            Debug.Log("pingPong : " + pingPong);
            if (pingPong)
            {
                
            }
        }
            
    }

    public void SendAllButtonSetting()
    {
        if (transform.name == "Button_Moter_1")
            SendMessage("SwitchMethod", (GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_1_State == "1") ? true : false);
        if (transform.name == "Button_Moter_2")
            SendMessage("SwitchMethod", (GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_2_State == "1") ? true : false);
        if (transform.name == "Button_Moter_3")
            SendMessage("SwitchMethod", (GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_3_State == "1") ? true : false);
        if (transform.name == "Button_Moter_4")
            SendMessage("SwitchMethod", (GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().Button_4_State == "1") ? true : false);
        if (transform.name == "Button_Power")
            SendMessage("SwitchMethod", (GameObject.Find("UI Root (3D)").GetComponent<MqttManager>().PowerButtonState == "1") ? true : false);
    }
}
