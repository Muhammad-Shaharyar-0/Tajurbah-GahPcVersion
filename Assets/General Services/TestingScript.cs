using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    public void OnValueChanged(string data)
    {
        Debug.Log("Value is Changed");
    }
    public void OnEndEdit(string data)
    {
        Debug.Log("On End Edit Value");
    }
}
