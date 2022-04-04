using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class EventWriter : MonoBehaviour
{
    WriteDataController writeDataControllerObject;

    [Header("Write Data in Start")]
    public bool writeInStart = false;

    [Header("Write Data on Enable")]
    public bool writeOnEnable = false;

    [Header("Write Data from InputField on Button Press")]
    public bool writeOnButtonClick = false;
    public Button buttonObj;
    private string checkPreviousData=null;

    [Header("Enter Event Name and Description")]
    public string eventName;
    public string eventDescription;

    void Start()
    {
        if (WriteDataController.Instance)
        {
            writeDataControllerObject = WriteDataController.Instance;
        }
        if(writeInStart)
        {
            SendData(eventName,eventDescription);
        }

        if(buttonObj!=null && writeOnButtonClick)
        {
            buttonObj.onClick.AddListener(SendDataFromInputField);
        }
    }

    private void OnEnable()
    {
        if (WriteDataController.Instance)
        {
            writeDataControllerObject = WriteDataController.Instance;
        }

        if (writeOnEnable)
        {
            SendData(eventName,eventDescription);
        }
    }

    public void SendData(string EventName, string EventDescription)
    {
        Analytics.CustomEvent(EventName, new Dictionary<string, object> { { EventName,EventDescription} });

        if (WriteDataController.Instance)
        {
            writeDataControllerObject.WriteEventData(EventName + ": " + EventDescription);
        }
        
    }

    public void SendData()
    {
        Analytics.CustomEvent(eventName, new Dictionary<string, object> { { eventName, eventDescription } });

        if (WriteDataController.Instance)
        {
            writeDataControllerObject.WriteEventData(eventName + ": " + eventDescription);
        }
    }

    void SendDataFromInputField()
    {
        if(checkPreviousData!= GetComponent<InputField>().text.ToString() && !string.IsNullOrEmpty(GetComponent<InputField>().text.ToString()))
        {
            SendData(eventName, GetComponent<InputField>().text.ToString());
            checkPreviousData = GetComponent<InputField>().text.ToString();
        }
    }

}
