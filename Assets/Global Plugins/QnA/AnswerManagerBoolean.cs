using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManagerBoolean : MonoBehaviour
{
    [SerializeField] GameObject ResponseTrue;
    [SerializeField] GameObject ResponseFalse;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Populate(bool? ans_boolean){
        if(null == ans_boolean){
            ResponseTrue.GetComponent<Toggle>().isOn = ResponseFalse.GetComponent<Toggle>().isOn = false;
        }
        else{
            ResponseFalse.GetComponent<Toggle>().isOn = !(ResponseTrue.GetComponent<Toggle>().isOn = (bool)ans_boolean);
        }
    }

    public bool? Response()
    {
        Debug.Log("true: " + ResponseTrue.GetComponent<Toggle>().isOn);
        Debug.Log("false: " + ResponseFalse.GetComponent<Toggle>().isOn);
        return ( (!ResponseTrue.GetComponent<Toggle>().isOn && !ResponseFalse.GetComponent<Toggle>().isOn) ? (bool?)null :  (bool?)ResponseTrue.GetComponent<Toggle>().isOn);
    }
}
