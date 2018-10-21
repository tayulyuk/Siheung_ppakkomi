using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButtonManager : MonoBehaviour {

    public GameObject moterButtonObject_1;
    public GameObject moterButtonObject_2;
    public GameObject moterButtonObject_3;
    public GameObject moterButtonObject_4;

    void Start()
    {
        MoterButtonState(false);
    }

    public void MoterButtonState(bool state)
    {
        moterButtonObject_1.SetActive(state);
        moterButtonObject_2.SetActive(state);
        moterButtonObject_3.SetActive(state);
        moterButtonObject_4.SetActive(state);
    }
}
