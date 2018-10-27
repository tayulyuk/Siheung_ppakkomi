﻿using UnityEngine;
using System.Collections;
using System.Net;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System;

public class MqttManager : MonoBehaviour
{
    private MqttClient client;    
    public string PowerButtonState;
    public string Button_1_State;
    public string Button_2_State;
    public string Button_3_State;
    public string Button_4_State;
    public bool isOne; // broad cast 사용하기 위해.

    public GameObject buttonPowerObject;
    public GameObject button1Object;
    public GameObject button2Object;
    public GameObject button3Object;
    public GameObject button4Object;
   
    void Start()
    {
        // create client instance 
        client = new MqttClient(IPAddress.Parse("119.205.235.214"), 1883, false, null);

        // register to message received 
        client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

        string clientId = Guid.NewGuid().ToString();
        client.Connect(clientId);

        // subscribe to the topic "/home/temperature" with QoS 2 
       // client.Subscribe(new string[] { "siheung/namu/result" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        client.Subscribe(new string[] { "siheung/namu/button1" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    /// <summary>
    /// 버튼 클릭 현재 정보를 전달한다.
    /// </summary>
    /// <param name="topic">버튼 주소</param>
    public void SendPublishButtonData(string topic,string sendData)
    {
        client.Publish("siheung/namu/" + topic, System.Text.Encoding.Default.GetBytes(sendData));     
    }
 
    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {        
        Debug.Log("M: " + System.Text.Encoding.UTF8.GetString(e.Message));
        ///test 끝나고 다시 연결해라.
        AllMessageParsing(System.Text.Encoding.UTF8.GetString(e.Message));    
        //각 버튼들 정렬 - 현재 받은 값으로
      //  AllButtonsSetting();      
    }

    void Update()
    {
        if (isOne)
        {
            AllButtonsSetting();
            isOne = false;          
        }
    }

    public void AllButtonsSetting()
    {
        buttonPowerObject.GetComponent<SwitchingManager>().SwitchMethod();
        button1Object.GetComponent<SwitchingManager>().SwitchMethod();
        button2Object.GetComponent<SwitchingManager>().SwitchMethod();
        button3Object.GetComponent<SwitchingManager>().SwitchMethod();
        button4Object.GetComponent<SwitchingManager>().SwitchMethod();
    }
       

    private void AllMessageParsing(string getMessage)
    {
        Button_1_State = GetParserString(getMessage, "|button1=", "|");
        Button_2_State = GetParserString(getMessage, "|button2=", "|");
        Button_3_State = GetParserString(getMessage, "|button3=", "|");
        Button_4_State = GetParserString(getMessage, "|button4=", "|");
        PowerButtonState = GetParserString(getMessage, "|buttonPower=", "|");
    }

    public string GetParserString(string message ,string startSearch,string endSearch)
    {
        string getValue = "";
        string search = "";

        search = startSearch;        
    
        int p = message.IndexOf(search);
        if (p >= 0)
        {
            // move forward to the value
            int start = p + search.Length;
            // now find the end by searching for the next closing tag starting at the start position, 
            // limiting the forward search to the max value length
            int end = 0;
            end = message.IndexOf(endSearch, start);           

            if (end >= 0)
            {
                // pull out the substring
                string v = message.Substring(start, end - start);
                // finally parse into a float
                // float value = float.Parse(v);
                // Debug.Log("1classTemp Value = " + value);
              
               getValue = v;                
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
