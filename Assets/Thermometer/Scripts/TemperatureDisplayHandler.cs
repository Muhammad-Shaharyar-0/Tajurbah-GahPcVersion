using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TemperatureDisplayHandler : MonoBehaviour
{

    [SerializeField] GameObject TempTextObj;
    [SerializeField] GameObject ReferencePositionObj;

    [SerializeField] TextMeshProUGUI TempText;

    
    void FixedUpdate()
    {
        TempTextObj.transform.position = ReferencePositionObj.transform.position;
        int temp = (int)this.GetComponentInParent<Tajurbah_Gah.TemperatureController>().CurrentTemp;
        TempText.text = "" + temp + "°C";
    }
}
