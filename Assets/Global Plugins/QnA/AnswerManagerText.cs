using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerManagerText : MonoBehaviour
{
    [SerializeField] GameObject ResponseText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Populate(string ans_text){
        if(!string.IsNullOrEmpty(ans_text)){
            ResponseText.GetComponent<InputField>().text = ans_text;
        }
        else{
            ResponseText.GetComponent<InputField>().text = string.Empty;
        }
    }

    public string Response()
    {
        return ResponseText.GetComponent<InputField>().text;
    }
}
