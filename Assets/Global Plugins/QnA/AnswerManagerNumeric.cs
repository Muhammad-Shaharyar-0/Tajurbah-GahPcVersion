using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManagerNumeric : MonoBehaviour
{
    [SerializeField] GameObject ResponseText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Populate(float? ans_numeric){
        if(null != ans_numeric){
            ResponseText.GetComponent<InputField>().text = ans_numeric.ToString();
        }
        else{
            ResponseText.GetComponent<InputField>().text = string.Empty;
        }
    }

    public float? Response()
    {
        string userVal = ResponseText.GetComponent<InputField>().text;
        float retVal;
        if(string.IsNullOrEmpty(userVal) || !float.TryParse(userVal, out retVal)){
            return null;
        }
        return retVal;
    }
}
