using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;

public class MqttManager : MonoBehaviour
{
    private MqttClient client;
    public string currentStateMessage;
    public string PowerButtonState;
    public string Button_1_State;
    public string Button_2_State;
    public string Button_3_State;
    public string Button_4_State;
   
    void Start()
    {
        // create client instance 
        client = new MqttClient(IPAddress.Parse("119.205.235.214"), 1883, false, null);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // subscribe to the topic "/home/temperature" with QoS 2 
        client.Subscribe(new string[] { "siheung/namu/buttonState" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    public void GetAllButtonDataFromArduino()
    {
       // client.Publish("siheung/namu/buttonState", System.Text.Encoding.Default.GetBytes("getButtoData"));
      //  GetAllMessageButtonSetting();
    }

    /// <summary>
    /// 아두이노로 부터 버튼 정보를 모두 받아 입력한다.
    /// </summary>
    void GetAllMessageButtonSetting()
    {
        PowerButtonState = GetParserMessageToButton("power", currentStateMessage);
        Button_1_State = GetParserMessageToButton("1button", currentStateMessage);
        Button_2_State = GetParserMessageToButton("2button", currentStateMessage);
        Button_3_State = GetParserMessageToButton("3button", currentStateMessage);
        Button_4_State = GetParserMessageToButton("4button", currentStateMessage);
    }

    private string GetParserMessageToButton(string order, string message)
    {
         string getValue = "";
        string search = "";
           
        if (order == "power")
        {
            search = ",power:";
        }
        if (order == "1button")
        {
            search = ",1button:";
        }
        if (order == "2button")
        {
            search = ",2button:";
        }
        if (order == "3button")
        {
            search = ",3button:";
        }
        if (order == "4button")
        {
            search = ",4button:";
        }
        int p = message.IndexOf(search);
        if (p >= 0)
        {
            // move forward to the value
            int start = p + search.Length;
            // now find the end by searching for the next closing tag starting at the start position, 
            // limiting the forward search to the max value length
            int end = 0;
                 end = message.IndexOf(",", start);

            if (end >= 0)
            {
                // pull out the substring
               getValue = message.Substring(start, end - start);
                // finally parse into a float
            }
            else
            {
                Debug.Log("Bad html - closing tag not found");
                Debug.Log(getValue);
                getValue = "text error";
            }
        }
        return getValue;
    }
   
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        currentStateMessage = System.Text.Encoding.UTF8.GetString(e.Message);
        Debug.Log("Received: " + System.Text.Encoding.UTF8.GetString(e.Message));
    }

    private string GetParserString(string message, string order)
    {
        string getValue = "";
        string search = "";

        if (order == "temp")
        {
            search = "\"temperature\":";
        }
        if (order == "humi")
        {
            search = "\"humidity\":";
        }
        int p = message.IndexOf(search);
        if (p >= 0)
        {
            // move forward to the value
            int start = p + search.Length;
            // now find the end by searching for the next closing tag starting at the start position, 
            // limiting the forward search to the max value length
            int end = 0;
            if (order == "temp")
                end = message.IndexOf(",", start);
            if (order == "humi")
                end = message.IndexOf("}", start);

            if (end >= 0)
            {
                // pull out the substring
                string v = message.Substring(start, end - start);
                // finally parse into a float
                // float value = float.Parse(v);
                // Debug.Log("1classTemp Value = " + value);
                if (order == "temp")
                    getValue = v + " ℃";
                if (order == "humi")
                    getValue = v + " ％";
            }
            else
            {
                Debug.Log("Bad html - closing tag not found");
                getValue = "text error";
            }
        }
        return getValue;
    }
}
